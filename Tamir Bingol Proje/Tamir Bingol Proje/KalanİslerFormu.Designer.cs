namespace Tamir_Bingol_Proje
{
    partial class KalanİslerFormu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richtxtTamirIsler = new RichTextBox();
            richtxtDagitimIsler = new RichTextBox();
            richtxtBitenIsler = new RichTextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // richtxtTamirIsler
            // 
            richtxtTamirIsler.Location = new Point(3, 43);
            richtxtTamirIsler.Name = "richtxtTamirIsler";
            richtxtTamirIsler.Size = new Size(625, 274);
            richtxtTamirIsler.TabIndex = 0;
            richtxtTamirIsler.Text = "";
            // 
            // richtxtDagitimIsler
            // 
            richtxtDagitimIsler.Location = new Point(3, 364);
            richtxtDagitimIsler.Name = "richtxtDagitimIsler";
            richtxtDagitimIsler.Size = new Size(625, 274);
            richtxtDagitimIsler.TabIndex = 1;
            richtxtDagitimIsler.Text = "";
            // 
            // richtxtBitenIsler
            // 
            richtxtBitenIsler.Location = new Point(669, 210);
            richtxtBitenIsler.Name = "richtxtBitenIsler";
            richtxtBitenIsler.Size = new Size(565, 274);
            richtxtBitenIsler.TabIndex = 2;
            richtxtBitenIsler.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.Location = new Point(3, 15);
            label1.Name = "label1";
            label1.Size = new Size(240, 25);
            label1.TabIndex = 3;
            label1.Text = "Tamir Biriminde Kalan İşler";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.Location = new Point(37, 336);
            label2.Name = "label2";
            label2.Size = new Size(271, 25);
            label2.TabIndex = 4;
            label2.Text = "Dağıtım Ünitesinde Kalan İşler";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label3.Location = new Point(669, 173);
            label3.Name = "label3";
            label3.Size = new Size(154, 25);
            label3.TabIndex = 5;
            label3.Text = "Biten İş Paketleri";
            // 
            // KalanİslerFormu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1246, 650);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(richtxtBitenIsler);
            Controls.Add(richtxtDagitimIsler);
            Controls.Add(richtxtTamirIsler);
            Name = "KalanİslerFormu";
            Text = "KalanİslerFormu";
            Load += KalanİslerFormu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richtxtTamirIsler;
        private RichTextBox richtxtDagitimIsler;
        private RichTextBox richtxtBitenIsler;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}