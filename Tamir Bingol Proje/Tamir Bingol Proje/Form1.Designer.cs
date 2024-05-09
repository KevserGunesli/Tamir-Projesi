namespace Tamir_Bingol_Proje
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnTamir = new Button();
            btnIsPaketi = new Button();
            richtxtbox = new RichTextBox();
            btnServis = new Button();
            SuspendLayout();
            // 
            // btnTamir
            // 
            btnTamir.Location = new Point(660, 18);
            btnTamir.Name = "btnTamir";
            btnTamir.Size = new Size(143, 34);
            btnTamir.TabIndex = 0;
            btnTamir.Text = "Tamir Birimlerini Hazırla";
            btnTamir.UseVisualStyleBackColor = true;
            btnTamir.Click += btnTamir_Click;
            // 
            // btnIsPaketi
            // 
            btnIsPaketi.Location = new Point(836, 16);
            btnIsPaketi.Name = "btnIsPaketi";
            btnIsPaketi.Size = new Size(143, 33);
            btnIsPaketi.TabIndex = 1;
            btnIsPaketi.Text = "İş Paketlerini Yükle";
            btnIsPaketi.UseVisualStyleBackColor = true;
            btnIsPaketi.Click += btnIsPaketi_Click;
            // 
            // richtxtbox
            // 
            richtxtbox.Location = new Point(660, 105);
            richtxtbox.Name = "richtxtbox";
            richtxtbox.ReadOnly = true;
            richtxtbox.Size = new Size(526, 289);
            richtxtbox.TabIndex = 2;
            richtxtbox.Text = "";
            // 
            // btnServis
            // 
            btnServis.Location = new Point(1011, 15);
            btnServis.Name = "btnServis";
            btnServis.Size = new Size(117, 40);
            btnServis.TabIndex = 3;
            btnServis.Text = "Servisi Başlat";
            btnServis.UseVisualStyleBackColor = true;
            btnServis.Click += btnServis_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1220, 820);
            Controls.Add(richtxtbox);
            Controls.Add(btnServis);
            Controls.Add(btnIsPaketi);
            Controls.Add(btnTamir);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnTamir;
        private Button btnIsPaketi;
        private RichTextBox richtxtbox;
        private Button button1;
        private Button btnServis;
    }
}
