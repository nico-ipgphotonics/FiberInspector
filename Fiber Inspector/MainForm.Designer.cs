namespace FiberInspector
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ComboBox cameraComboBox;
        private System.Windows.Forms.Button startStopButton;
        private System.Windows.Forms.Button captureFrameButton;
        private System.Windows.Forms.TrackBar brightnessSlider;
        private System.Windows.Forms.TrackBar contrastSlider;
        private System.Windows.Forms.TrackBar colorSlider;
        private System.Windows.Forms.CheckBox grayscaleCheckBox;
        private System.Windows.Forms.CheckBox flipHorizontalCheckBox;
        private System.Windows.Forms.CheckBox flipVerticalCheckBox;
        private System.Windows.Forms.CheckBox noiseReductionCheckBox;
        private System.Windows.Forms.Label Brightness;
        private System.Windows.Forms.Label Contrast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Saturation;
        private System.Windows.Forms.TrackBar sharpnessSlider;
        private System.Windows.Forms.TrackBar zoomSlider;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pictureBox = new PictureBox();
            cameraComboBox = new ComboBox();
            startStopButton = new Button();
            captureFrameButton = new Button();
            brightnessSlider = new TrackBar();
            contrastSlider = new TrackBar();
            colorSlider = new TrackBar();
            grayscaleCheckBox = new CheckBox();
            flipHorizontalCheckBox = new CheckBox();
            flipVerticalCheckBox = new CheckBox();
            noiseReductionCheckBox = new CheckBox();
            Brightness = new Label();
            Contrast = new Label();
            Saturation = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            sharpnessSlider = new TrackBar();
            zoomSlider = new TrackBar();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)brightnessSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)contrastSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)colorSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sharpnessSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)zoomSlider).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = BorderStyle.Fixed3D;
            pictureBox.Location = new Point(87, 12);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(640, 480);
            pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // cameraComboBox
            // 
            cameraComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            cameraComboBox.FormattingEnabled = true;
            cameraComboBox.Location = new Point(12, 510);
            cameraComboBox.Name = "cameraComboBox";
            cameraComboBox.Size = new Size(121, 28);
            cameraComboBox.TabIndex = 1;
            // 
            // startStopButton
            // 
            startStopButton.Location = new Point(161, 508);
            startStopButton.Name = "startStopButton";
            startStopButton.Size = new Size(84, 30);
            startStopButton.TabIndex = 2;
            startStopButton.Text = "Start Capture";
            startStopButton.UseVisualStyleBackColor = true;
            startStopButton.Click += StartStopButton_Click;
            // 
            // captureFrameButton
            // 
            captureFrameButton.Location = new Point(550, 565);
            captureFrameButton.Name = "captureFrameButton";
            captureFrameButton.Size = new Size(89, 30);
            captureFrameButton.TabIndex = 3;
            captureFrameButton.Text = "Shot Pic";
            captureFrameButton.UseVisualStyleBackColor = true;
            captureFrameButton.Click += CaptureFrameButton_Click;
            // 
            // brightnessSlider
            // 
            brightnessSlider.Location = new Point(12, 572);
            brightnessSlider.Maximum = 100;
            brightnessSlider.Name = "brightnessSlider";
            brightnessSlider.Size = new Size(121, 56);
            brightnessSlider.TabIndex = 7;
            brightnessSlider.Tag = "";
            brightnessSlider.TickFrequency = 10;
            brightnessSlider.Value = 50;
            // 
            // contrastSlider
            // 
            contrastSlider.Location = new Point(150, 572);
            contrastSlider.Maximum = 100;
            contrastSlider.Name = "contrastSlider";
            contrastSlider.Size = new Size(121, 56);
            contrastSlider.TabIndex = 8;
            contrastSlider.TickFrequency = 10;
            contrastSlider.Value = 50;
            // 
            // colorSlider
            // 
            colorSlider.Location = new Point(290, 572);
            colorSlider.Maximum = 100;
            colorSlider.Name = "colorSlider";
            colorSlider.Size = new Size(121, 56);
            colorSlider.TabIndex = 9;
            colorSlider.TickFrequency = 10;
            colorSlider.Value = 50;
            // 
            // grayscaleCheckBox
            // 
            grayscaleCheckBox.AutoSize = true;
            grayscaleCheckBox.Location = new Point(281, 514);
            grayscaleCheckBox.Name = "grayscaleCheckBox";
            grayscaleCheckBox.Size = new Size(94, 24);
            grayscaleCheckBox.TabIndex = 4;
            grayscaleCheckBox.Text = "Grayscale";
            grayscaleCheckBox.UseVisualStyleBackColor = true;
            // 
            // flipHorizontalCheckBox
            // 
            flipHorizontalCheckBox.AutoSize = true;
            flipHorizontalCheckBox.Location = new Point(392, 514);
            flipHorizontalCheckBox.Name = "flipHorizontalCheckBox";
            flipHorizontalCheckBox.Size = new Size(129, 24);
            flipHorizontalCheckBox.TabIndex = 5;
            flipHorizontalCheckBox.Text = "Flip Horizontal";
            flipHorizontalCheckBox.UseVisualStyleBackColor = true;
            // 
            // flipVerticalCheckBox
            // 
            flipVerticalCheckBox.AutoSize = true;
            flipVerticalCheckBox.Location = new Point(531, 514);
            flipVerticalCheckBox.Name = "flipVerticalCheckBox";
            flipVerticalCheckBox.Size = new Size(108, 24);
            flipVerticalCheckBox.TabIndex = 6;
            flipVerticalCheckBox.Text = "Flip Vertical";
            flipVerticalCheckBox.UseVisualStyleBackColor = true;
            // 
            // noiseReductionCheckBox
            // 
            noiseReductionCheckBox.AutoSize = true;
            noiseReductionCheckBox.Location = new Point(645, 514);
            noiseReductionCheckBox.Name = "noiseReductionCheckBox";
            noiseReductionCheckBox.Size = new Size(140, 24);
            noiseReductionCheckBox.TabIndex = 16;
            noiseReductionCheckBox.Text = "Noise Reduction";
            noiseReductionCheckBox.UseVisualStyleBackColor = true;
            // 
            // Brightness
            // 
            Brightness.AutoSize = true;
            Brightness.Location = new Point(35, 549);
            Brightness.Name = "Brightness";
            Brightness.Size = new Size(77, 20);
            Brightness.TabIndex = 10;
            Brightness.Text = "Brightness";
            // 
            // Contrast
            // 
            Contrast.AutoSize = true;
            Contrast.Location = new Point(181, 549);
            Contrast.Name = "Contrast";
            Contrast.Size = new Size(64, 20);
            Contrast.TabIndex = 11;
            Contrast.Text = "Contrast";
            // 
            // Saturation
            // 
            Saturation.AutoSize = true;
            Saturation.Location = new Point(312, 549);
            Saturation.Name = "Saturation";
            Saturation.Size = new Size(77, 20);
            Saturation.TabIndex = 12;
            Saturation.Text = "Saturation";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(461, 601);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 12;
            textBox1.Text = "Customer";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(461, 634);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 14;
            textBox2.Text = "SN";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(614, 601);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 13;
            textBox3.Text = "Fiber Type";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(614, 634);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(125, 27);
            textBox4.TabIndex = 15;
            textBox4.Text = "Result";
            // 
            // sharpnessSlider
            // 
            sharpnessSlider.Location = new Point(12, 634);
            sharpnessSlider.Maximum = 53;
            sharpnessSlider.Minimum = 50;
            sharpnessSlider.Name = "sharpnessSlider";
            sharpnessSlider.Size = new Size(121, 56);
            sharpnessSlider.TabIndex = 10;
            sharpnessSlider.Value = 50;
            sharpnessSlider.ValueChanged += SharpnessSlider_ValueChanged;
            // 
            // zoomSlider
            // 
            zoomSlider.Location = new Point(161, 634);
            zoomSlider.Maximum = 200;
            zoomSlider.Minimum = 100;
            zoomSlider.Name = "zoomSlider";
            zoomSlider.Size = new Size(121, 56);
            zoomSlider.TabIndex = 11;
            zoomSlider.TickFrequency = 10;
            zoomSlider.Value = 100;
            zoomSlider.ValueChanged += ZoomSlider_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 611);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 19;
            label1.Text = "Sharpness";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(196, 611);
            label2.Name = "label2";
            label2.Size = new Size(49, 20);
            label2.TabIndex = 20;
            label2.Text = "Zoom";
            // 
            // MainForm
            // 
            ClientSize = new Size(819, 710);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(Saturation);
            Controls.Add(Contrast);
            Controls.Add(Brightness);
            Controls.Add(pictureBox);
            Controls.Add(cameraComboBox);
            Controls.Add(startStopButton);
            Controls.Add(captureFrameButton);
            Controls.Add(brightnessSlider);
            Controls.Add(contrastSlider);
            Controls.Add(colorSlider);
            Controls.Add(grayscaleCheckBox);
            Controls.Add(flipHorizontalCheckBox);
            Controls.Add(flipVerticalCheckBox);
            Controls.Add(noiseReductionCheckBox);
            Controls.Add(sharpnessSlider);
            Controls.Add(zoomSlider);
            Name = "MainForm";
            Text = "Fiber Inspector";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)brightnessSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)contrastSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)colorSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)sharpnessSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)zoomSlider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
