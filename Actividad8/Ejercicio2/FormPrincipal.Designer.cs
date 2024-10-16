namespace Ejercicio2
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnImportarCuentas = new System.Windows.Forms.Button();
            this.btnExportarCuentas = new System.Windows.Forms.Button();
            this.btnVerCuentas = new System.Windows.Forms.Button();
            this.tbVer = new System.Windows.Forms.TextBox();
            this.btnResguardar = new System.Windows.Forms.Button();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnImportarCuentas
            // 
            this.btnImportarCuentas.Location = new System.Drawing.Point(569, 61);
            this.btnImportarCuentas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportarCuentas.Name = "btnImportarCuentas";
            this.btnImportarCuentas.Size = new System.Drawing.Size(173, 35);
            this.btnImportarCuentas.TabIndex = 2;
            this.btnImportarCuentas.Text = "2- Importar cuentas";
            this.btnImportarCuentas.UseVisualStyleBackColor = true;
            this.btnImportarCuentas.Click += new System.EventHandler(this.btnImportarCuentas_Click);
            // 
            // btnExportarCuentas
            // 
            this.btnExportarCuentas.Location = new System.Drawing.Point(569, 106);
            this.btnExportarCuentas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExportarCuentas.Name = "btnExportarCuentas";
            this.btnExportarCuentas.Size = new System.Drawing.Size(173, 35);
            this.btnExportarCuentas.TabIndex = 3;
            this.btnExportarCuentas.Text = "3- Exportar cuentas";
            this.btnExportarCuentas.UseVisualStyleBackColor = true;
            this.btnExportarCuentas.UseWaitCursor = true;
            this.btnExportarCuentas.Click += new System.EventHandler(this.btnExportarCuentas_Click);
            // 
            // btnVerCuentas
            // 
            this.btnVerCuentas.Location = new System.Drawing.Point(569, 16);
            this.btnVerCuentas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnVerCuentas.Name = "btnVerCuentas";
            this.btnVerCuentas.Size = new System.Drawing.Size(173, 35);
            this.btnVerCuentas.TabIndex = 4;
            this.btnVerCuentas.Text = "1- Ver cuentas";
            this.btnVerCuentas.UseVisualStyleBackColor = true;
            this.btnVerCuentas.Click += new System.EventHandler(this.btnVerCuentas_Click);
            // 
            // tbVer
            // 
            this.tbVer.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVer.Location = new System.Drawing.Point(2, 7);
            this.tbVer.Multiline = true;
            this.tbVer.Name = "tbVer";
            this.tbVer.ReadOnly = true;
            this.tbVer.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbVer.Size = new System.Drawing.Size(560, 257);
            this.tbVer.TabIndex = 5;
            // 
            // btnResguardar
            // 
            this.btnResguardar.Location = new System.Drawing.Point(569, 151);
            this.btnResguardar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnResguardar.Name = "btnResguardar";
            this.btnResguardar.Size = new System.Drawing.Size(173, 52);
            this.btnResguardar.TabIndex = 6;
            this.btnResguardar.Text = "4- Resguardar (Backup)";
            this.btnResguardar.UseVisualStyleBackColor = true;
            this.btnResguardar.Click += new System.EventHandler(this.btnResguardar_Click);
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Location = new System.Drawing.Point(569, 213);
            this.btnRestaurar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(173, 51);
            this.btnRestaurar.TabIndex = 7;
            this.btnRestaurar.Text = "5- Restaurar(Restore)";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 271);
            this.Controls.Add(this.btnRestaurar);
            this.Controls.Add(this.btnResguardar);
            this.Controls.Add(this.tbVer);
            this.Controls.Add(this.btnVerCuentas);
            this.Controls.Add(this.btnExportarCuentas);
            this.Controls.Add(this.btnImportarCuentas);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ejercicio2 - exportable";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnImportarCuentas;
        private System.Windows.Forms.Button btnExportarCuentas;
        private System.Windows.Forms.Button btnVerCuentas;
        private System.Windows.Forms.TextBox tbVer;
        private System.Windows.Forms.Button btnResguardar;
        private System.Windows.Forms.Button btnRestaurar;
    }
}

