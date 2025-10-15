
namespace TPWinForm_equipo_4B
{
    partial class frmABMCategorias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmABMCategorias));
            this.dgvCategorias = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCategoria = new System.Windows.Forms.TextBox();
            this.btnAgregarCat = new System.Windows.Forms.Button();
            this.btnEditarCat = new System.Windows.Forms.Button();
            this.btnEliminarCat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCategorias
            // 
            this.dgvCategorias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategorias.Location = new System.Drawing.Point(12, 12);
            this.dgvCategorias.Name = "dgvCategorias";
            this.dgvCategorias.Size = new System.Drawing.Size(335, 233);
            this.dgvCategorias.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 277);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre Categoria:";
            // 
            // tbCategoria
            // 
            this.tbCategoria.Location = new System.Drawing.Point(131, 274);
            this.tbCategoria.Name = "tbCategoria";
            this.tbCategoria.Size = new System.Drawing.Size(215, 20);
            this.tbCategoria.TabIndex = 2;
            // 
            // btnAgregarCat
            // 
            this.btnAgregarCat.BackColor = System.Drawing.Color.Wheat;
            this.btnAgregarCat.Location = new System.Drawing.Point(12, 312);
            this.btnAgregarCat.Name = "btnAgregarCat";
            this.btnAgregarCat.Size = new System.Drawing.Size(92, 31);
            this.btnAgregarCat.TabIndex = 3;
            this.btnAgregarCat.Text = "Agregar";
            this.btnAgregarCat.UseVisualStyleBackColor = false;
            this.btnAgregarCat.Click += new System.EventHandler(this.btnAgregarCat_Click);
            // 
            // btnEditarCat
            // 
            this.btnEditarCat.BackColor = System.Drawing.Color.Wheat;
            this.btnEditarCat.Location = new System.Drawing.Point(131, 312);
            this.btnEditarCat.Name = "btnEditarCat";
            this.btnEditarCat.Size = new System.Drawing.Size(92, 31);
            this.btnEditarCat.TabIndex = 4;
            this.btnEditarCat.Text = "Editar";
            this.btnEditarCat.UseVisualStyleBackColor = false;
            this.btnEditarCat.Click += new System.EventHandler(this.btnEditarCat_Click);
            // 
            // btnEliminarCat
            // 
            this.btnEliminarCat.BackColor = System.Drawing.Color.Wheat;
            this.btnEliminarCat.Location = new System.Drawing.Point(254, 312);
            this.btnEliminarCat.Name = "btnEliminarCat";
            this.btnEliminarCat.Size = new System.Drawing.Size(92, 31);
            this.btnEliminarCat.TabIndex = 5;
            this.btnEliminarCat.Text = "Eliminar";
            this.btnEliminarCat.UseVisualStyleBackColor = false;
            this.btnEliminarCat.Click += new System.EventHandler(this.btnEliminarCat_Click);
            // 
            // frmABMCategorias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(359, 357);
            this.Controls.Add(this.btnEliminarCat);
            this.Controls.Add(this.btnEditarCat);
            this.Controls.Add(this.btnAgregarCat);
            this.Controls.Add(this.tbCategoria);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCategorias);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(375, 396);
            this.MinimumSize = new System.Drawing.Size(375, 396);
            this.Name = "frmABMCategorias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listado de Categorías";
            this.Load += new System.EventHandler(this.frmABMCategorias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCategoria;
        private System.Windows.Forms.Button btnAgregarCat;
        private System.Windows.Forms.Button btnEditarCat;
        private System.Windows.Forms.Button btnEliminarCat;
    }
}