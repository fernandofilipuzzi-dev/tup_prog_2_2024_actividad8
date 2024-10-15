using Ejercicio1.Models;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Ejercicio1
{
    public partial class FormPrincipal : Form
    {
        Banco banco = new Banco();

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void btnVerCuentas_Click(object sender, EventArgs e)
        {
            tbVer.Clear();

            tbVer.Text += $"{"Número cuenta",20}|{"Nombre",-20}|{"Saldo",10}" + Environment.NewLine;
            tbVer.Text += $"{"-".PadLeft(19, '-'),20}|{"-".PadLeft(19, '-'),-20}|{"-".PadLeft(9, '-'),10}" + Environment.NewLine;
            for (int idx = 0; idx < banco.CantidadCuentas; idx++)
            {
                Cuenta cuenta = banco.VerCuenta(idx);
                tbVer.Text += $"{cuenta.Numero,20}|{cuenta.Titula.Nombre,20}|{cuenta.Saldo,10:f2}"+Environment.NewLine;
            }
        }

        private void btnImportarCuentas_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Importación de cuentas";
            openFileDialog1.Filter = "fichero csv|*.csv";
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                string path = openFileDialog1.FileName;

                FileStream fs = null;
                StreamReader sr = null;
                try
                {
                    fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    
                    sr=new StreamReader(fs);

                    //descarto la cabecera
                    sr.ReadLine();

                    while (sr.EndOfStream == false)
                    {
                        string linea = sr.ReadLine();

                        #region parseo
                        string[] campos = linea.Split(';');

                        int dni = Convert.ToInt32(campos[0].Trim());
                        string nombre=campos[1].Trim();
                        int numeroCuenta= Convert.ToInt32(campos[2].Trim());
                        double saldo= Convert.ToDouble(campos[3].Trim());
                        #endregion

                        #region agregar/actualizar
                        Cuenta cuenta = banco.VerCuentaPorNumero(numeroCuenta);
                        if (cuenta == null)
                        {
                            cuenta=banco.AgregarCuenta(dni, nombre, numeroCuenta);
                        }
                        cuenta.Saldo = saldo;
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al importar la cuenta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally 
                {
                    if(sr!=null) sr.Close();
                    if(fs!=null) sr.Close();
                }

                btnVerCuentas.PerformClick();
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Exportación de cuentas que superaron $10000";
            saveFileDialog1.Filter = "fichero csv|*.csv";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;

                FileStream fs = null;
                StreamWriter sr = null;
                try
                {
                    fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                    sr = new StreamWriter(fs);

                    string linea = $"DNI; nombre; número de cuenta; saldo";
                    sr.WriteLine(linea);

                    for (int idx = 0; idx < banco.CantidadCuentas; idx++)
                    {
                        Cuenta cuenta = banco.VerCuenta(idx);

                        if (cuenta.Saldo >= 10000)
                        {
                            linea = $"{cuenta.Titula.DNI};{cuenta.Titula.Nombre};{cuenta.Numero};{cuenta.Saldo:f2}";
                            sr.WriteLine(linea);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al importar la cuenta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (sr != null) sr.Close();
                    if (fs != null) sr.Close();
                }

                btnVerCuentas.PerformClick();
            }
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream("banco.dat", FileMode.OpenOrCreate, FileAccess.Read);
                
                BinaryFormatter bf = new BinaryFormatter();

                if(fs.Length>0)
                    banco= bf.Deserialize(fs) as Banco;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al levantar el contexto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { 
                if(fs!=null) fs.Close();
            }

            #region inicialización y actualización interfaz
            if (banco==null)
            { 
                banco= new Banco();
            }
            btnVerCuentas.PerformClick();
            #endregion
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream("banco.dat", FileMode.OpenOrCreate, FileAccess.Write);

                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(fs, banco);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al persistir el contexto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
    }
}
