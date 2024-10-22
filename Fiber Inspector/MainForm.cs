using DirectShowLib;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace FiberInspector
{
    public partial class MainForm : Form
    {
        private VideoCapture _capture = null;
        private bool _isCapturing = false;
        private CancellationTokenSource _cancellationTokenSource;
        private const int FrameDelay = 33; // ~30 FPS
        private object _locker = new object();

        public MainForm()
        {
            InitializeComponent();
            LoadCameras();
        }

        // Carica le telecamere disponibili nella ComboBox
        private void LoadCameras()
        {
            cameraComboBox.Items.Clear();
            DsDevice[] systemCameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            for (int i = 0; i < systemCameras.Length; i++)
            {
                cameraComboBox.Items.Add(new CameraInfo { Index = i, Name = systemCameras[i].Name });
            }

            if (cameraComboBox.Items.Count > 0)
            {
                cameraComboBox.SelectedIndex = 0; // Seleziona la prima telecamera per impostazione predefinita
            }
            else
            {
                MessageBox.Show("Nessuna telecamera trovata.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Gestisce il click del pulsante Start/Stop Capture
        private async void StartStopButton_Click(object sender, EventArgs e)
        {
            if (!_isCapturing)
            {
                try
                {
                    CameraInfo selectedCamera = (CameraInfo)cameraComboBox.SelectedItem;
                    int cameraIndex = selectedCamera.Index;

                    // Apre la telecamera selezionata
                    _capture = new VideoCapture(cameraIndex);
                    _capture.Set(CapProp.FrameWidth, 640); // Imposta la risoluzione desiderata
                    _capture.Set(CapProp.FrameHeight, 480);

                    _isCapturing = true;
                    Invoke(new Action(() =>
                    {
                        startStopButton.Text = "Stop Capture"; // Modifica l'interfaccia utente sul thread principale
                    }));

                    _cancellationTokenSource = new CancellationTokenSource();
                    await Task.Run(() => CaptureLoop(_cancellationTokenSource.Token));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore nell'apertura della telecamera: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                StopCapture();
            }
        }

        // Ferma la cattura video
        private void StopCapture()
        {
            _cancellationTokenSource?.Cancel();
            _capture?.Dispose();
            _isCapturing = false;
            Invoke(new Action(() =>
            {
                startStopButton.Text = "Start Capture"; // Modifica l'interfaccia utente sul thread principale
            }));
        }

        // Ciclo di cattura del video
        private void CaptureLoop(CancellationToken token)
        {
            double zoomFactor = 1.0;

            while (!token.IsCancellationRequested)
            {
                lock (_locker)
                {
                    if (_capture != null && _capture.IsOpened)
                    {
                        try
                        {
                            using (Mat frame = _capture.QueryFrame())
                            {
                                if (frame == null || frame.IsEmpty)
                                {
                                    continue; // Salta il ciclo se non c'è un frame valido
                                }

                                // Applica il fattore di zoom
                                Invoke(new Action(() =>
                                {
                                    zoomFactor = zoomSlider.Value / 100.0; // Scala il valore dello slider
                                }));

                                if (zoomFactor != 1.0)
                                {
                                    CvInvoke.Resize(frame, frame, Size.Empty, zoomFactor, zoomFactor, Inter.Linear);
                                }

                                ApplyAdjustments(frame);
                                ApplyEffects(frame);

                                // Aggiorna la PictureBox sull'interfaccia utente principale
                                Invoke(new Action(() =>
                                {
                                    if (pictureBox.Image != null)
                                    {
                                        pictureBox.Image.Dispose(); // Rilascia l'immagine precedente
                                    }
                                    pictureBox.Image = frame.ToBitmap(); // Aggiorna il flusso video in tempo reale
                                }));

                                Thread.Sleep(FrameDelay); // Controlla la frequenza dei frame
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Errore durante la cattura del frame: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            StopCapture();
                        }
                    }
                }
            }
        }

        // Applica le regolazioni di luminosità, contrasto e colore al frame
        private void ApplyAdjustments(Mat frame)
        {
            double brightness = 0;
            double contrast = 0;
            double colorAdjust = 0;
            double sharpnessValue = 0;

            // Ottieni i valori degli slider in modo sicuro dal thread principale
            Invoke(new Action(() =>
            {
                brightness = brightnessSlider.Value / 50.0 - 1.0; // Intervallo da -1 a +1
                contrast = contrastSlider.Value / 50.0;           // Intervallo da 0 a +2
                colorAdjust = colorSlider.Value / 50.0;           // Regolazione della saturazione del colore
                sharpnessValue = (sharpnessSlider.Value - 50) / 100.0;  // Intervallo più graduale da -0.5 a +0.5
            }));

            // Regola luminosità e contrasto
            frame.ConvertTo(frame, DepthType.Cv8U, contrast, brightness * 50);

            // Regola il colore
            if (colorAdjust != 1.0)
            {
                CvInvoke.CvtColor(frame, frame, ColorConversion.Bgr2Hsv);
                using (VectorOfMat channels = new VectorOfMat())
                {
                    CvInvoke.Split(frame, channels);
                    using (Mat saturationChannel = new Mat())
                    {
                        channels[1].CopyTo(saturationChannel);
                        CvInvoke.Multiply(saturationChannel, new ScalarArray(colorAdjust), saturationChannel);
                        saturationChannel.CopyTo(channels[1]);
                    }
                    CvInvoke.Merge(channels, frame);
                }
                CvInvoke.CvtColor(frame, frame, ColorConversion.Hsv2Bgr);
            }

            // Applica nitidezza o levigatura in modo graduale
            if (sharpnessValue > 0)
            {
                // Nitidezza: Applica un kernel di nitidezza con effetto graduale
                Mat kernel = new Mat(new Size(3, 3), DepthType.Cv32F, 1);
                float[] sharpKernelValues = {
                    -1, -1, -1,
                    -1, 9 * (float)(1 + sharpnessValue), -1,  // Nitidezza graduale basata sul valore di sharpnessValue
                    -1, -1, -1
                };
                kernel.SetTo(sharpKernelValues);
                CvInvoke.Filter2D(frame, frame, kernel, new Point(-1, -1));
            }
            else if (sharpnessValue < 0)
            {
                // Levigatura: Applica un effetto di sfocatura gaussiana graduale
                int blurSize = (int)(7 * (1.0 + sharpnessValue)); // Dimensione sfocatura più piccola per una levigatura graduale
                if (blurSize % 2 == 0) blurSize += 1;  // La dimensione della sfocatura deve essere dispari
                CvInvoke.GaussianBlur(frame, frame, new Size(blurSize, blurSize), 0);
            }
        }

        // Applica effetti in scala di grigi, ribaltamento e riduzione del rumore
        private void ApplyEffects(Mat frame)
        {
            bool grayscale = false;
            bool flipHorizontal = false;
            bool flipVertical = false;
            bool noiseReduction = false;

            // Ottieni i valori delle checkbox in modo sicuro dal thread principale
            Invoke(new Action(() =>
            {
                grayscale = grayscaleCheckBox.Checked;
                flipHorizontal = flipHorizontalCheckBox.Checked;
                flipVertical = flipVerticalCheckBox.Checked;
                noiseReduction = noiseReductionCheckBox.Checked;
            }));

            if (grayscale)
            {
                CvInvoke.CvtColor(frame, frame, ColorConversion.Bgr2Gray);
                CvInvoke.CvtColor(frame, frame, ColorConversion.Gray2Bgr); // Converte nuovamente a BGR per consistenza
            }

            if (flipHorizontal)
            {
                CvInvoke.Flip(frame, frame, FlipType.Horizontal);
            }

            if (flipVertical)
            {
                CvInvoke.Flip(frame, frame, FlipType.Vertical);
            }

            // Applica riduzione del rumore se abilitata
            if (noiseReduction)
            {
                // Sfocatura gaussiana per riduzione del rumore
                CvInvoke.GaussianBlur(frame, frame, new Size(5, 5), 0);
            }
        }

        // Cattura e salva il frame con un watermark
        private void CaptureFrameButton_Click(object sender, EventArgs e)
        {
            if (_capture != null && _capture.IsOpened)
            {
                try
                {
                    using (Mat frame = _capture.QueryFrame())
                    {
                        if (frame != null)
                        {
                            string customer = string.Empty;
                            string serialNumber = string.Empty;
                            string fiberType = string.Empty;
                            string result = string.Empty;

                            // Recupera i testi dalla UI nel thread principale
                            Invoke(new Action(() =>
                            {
                                customer = textBox1.Text;
                                serialNumber = textBox2.Text;
                                fiberType = textBox3.Text;
                                result = textBox4.Text;
                            }));

                            // Applica tutte le regolazioni e gli effetti al frame
                            ApplyAdjustments(frame);
                            ApplyEffects(frame);

                            // Aggiungi i watermark (cliente, numero di serie, tipo, risultato, timestamp)
                            AddWatermarks(frame, customer, serialNumber, fiberType, result);

                            // Salva il frame modificato come file immagine
                            string filename = $"{customer}_{DateTime.Now:ddMMyyyy_HHmmss}_{result}.jpg";
                            frame.Save(filename);

                            MessageBox.Show($"Frame salvato come {filename}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore durante la cattura del frame: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("La cattura video non è attiva.", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Aggiunge watermark (cliente, SN, tipo, risultato e timestamp) al frame
        private void AddWatermarks(Mat frame, string customer, string serialNumber, string fiberType, string result)
        {
            var font = FontFace.HersheySimplex;
            double fontScale = 0.5;
            int thickness = 1;
            var color = new MCvScalar(255, 255, 255); // Colore bianco per il testo

            int width = frame.Width;
            int height = frame.Height;

            // Aggiungi i watermark con testo personalizzato in diverse posizioni del frame
            CvInvoke.PutText(frame, $"Cliente: {customer}", new Point(5, height - 70), font, fontScale, color, thickness);
            CvInvoke.PutText(frame, $"SN: {serialNumber}", new Point(5, height - 50), font, fontScale, color, thickness);
            CvInvoke.PutText(frame, $"Tipo: {fiberType}", new Point(5, height - 30), font, fontScale, color, thickness);
            CvInvoke.PutText(frame, $"Risultato: {result}", new Point(5, height - 10), font, fontScale, color, thickness);

            // Aggiungi il timestamp in alto a sinistra
            string timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            int baseline = 0;
            var timestampSize = CvInvoke.GetTextSize(timestamp, font, fontScale, thickness, ref baseline);

            // Posiziona il timestamp nell'angolo in alto a sinistra
            CvInvoke.PutText(frame, timestamp, new Point(5, timestampSize.Height + 5), font, fontScale, color, thickness);
        }

        // Classe CameraInfo usata per rappresentare le telecamere nella ComboBox
        public class CameraInfo
        {
            public int Index { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name; // Mostra il nome della telecamera nella ComboBox
            }
        }

        private void SharpnessSlider_ValueChanged(object sender, EventArgs e)
        {
            // Triggered quando i valori dello slider di nitidezza cambiano
            // Lasciare vuoto poiché l'effetto viene applicato all'interno del CaptureLoop
        }

        private void ZoomSlider_ValueChanged(object sender, EventArgs e)
        {
            // Triggered quando i valori dello slider zoom cambiano
            // Lasciare vuoto poiché l'effetto viene applicato all'interno del CaptureLoop
        }

    }
}
