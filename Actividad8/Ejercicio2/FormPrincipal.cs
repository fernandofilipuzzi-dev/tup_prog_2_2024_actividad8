using Ejercicio2.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Ejercicio2
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
                tbVer.Text += $"{cuenta.Numero,20}|{cuenta.Titular.Nombre,20}|{cuenta.Saldo,10:f2}" + Environment.NewLine;
            }
        }

        private void btnImportarCuentas_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Importación de cuentas";
            openFileDialog1.Filter = "fichero csv|*.csv";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;

                FileStream fs = null;
                StreamReader sr = null;
                try
                {
                    fs = new FileStream(path, FileMode.Open, FileAccess.Read);

                    sr = new StreamReader(fs);

                    //descarto la cabecera
                    sr.ReadLine();

                    while (sr.EndOfStream == false)
                    {
                        string linea = sr.ReadLine();

                        #region parseo
                        //al implementar IExportables actuan como un helper, me ayudan a parsear los registros
                        //En este caso particular - viene en la misma linea los dos registros
                        //cada entidad sabrá que sacar de cada línea.
                        Cuenta c = new Cuenta();
                        c.Leer(linea);
                        Persona p = new Persona();
                        p.Leer(linea);
                        #endregion

                        #region parseo 
                        //ahora lo resuelvo con la implementación de las interfaces

                        //string[] campos = linea.Split(';');

                        //int dni = Convert.ToInt32(campos[0].Trim());
                        //string nombre=campos[1].Trim();
                        //int numeroCuenta= Convert.ToInt32(campos[2].Trim());
                        //double saldo= Convert.ToDouble(campos[3].Trim());
                        #endregion

                        #region agregar/actualizar
                        //Cuenta cuenta = banco.VerCuentaPorNumero(numeroCuenta;
                        Cuenta cuenta = banco.VerCuentaPorNumero(c.Numero);
                        if (cuenta == null)
                        {
                            //cuenta=banco.AgregarCuenta(dni, nombre, numero);
                            cuenta = banco.AgregarCuenta(p.DNI, p.Nombre, c.Numero);
                        }
                        //cuenta.Saldo = saldo;
                        cuenta.Saldo = c.Saldo;
                        #endregion
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

        private void btnExportarCuentas_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Exportación de cuentas que superaron $10000";
            saveFileDialog1.Filter = "fichero csv|*.csv";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;

                FileStream fs = null;
                StreamWriter sw = null;
                try
                {
                    fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                    sw = new StreamWriter(fs);

                    #region ciclo de escritura
                    string linea = $"DNI; nombre; número de cuenta; saldo";
                    sw.WriteLine(linea);

                    for (int idx = 0; idx < banco.CantidadCuentas; idx++)
                    {
                        Cuenta cuenta = banco.VerCuenta(idx);

                        if (cuenta.Saldo >= 10000)
                        {
                            //en la parte 1 tenia que:
                            //linea = $"{cuenta.Titula.DNI};{cuenta.Titula.Nombre};{cuenta.Numero};{cuenta.Saldo:f2}";

                            //ahora sería
                            linea = cuenta.Escribir();
                            sw.WriteLine(linea);
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al importar la cuenta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (sw != null) sw.Close();
                    if (fs != null) fs.Close();
                }

                btnVerCuentas.PerformClick();
            }
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            #region deserialización - restaurando contexto
            FileStream fs = null;
            try
            {
                fs = new FileStream("ejercicio1.dat", FileMode.OpenOrCreate, FileAccess.Read);

                BinaryFormatter bf = new BinaryFormatter();

                if (fs.Length > 0)
                    banco = bf.Deserialize(fs) as Banco;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al levantar el contexto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            #endregion

            #region  preinicialización y actualización de interfaz
            openFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.InitialDirectory = Application.StartupPath;

            if (banco == null)
            {
                banco = new Banco();
            }
            btnVerCuentas.PerformClick();
            #endregion
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            #region serialización - salvando el contexto
            FileStream fs = null;
            try
            {
                fs = new FileStream("ejercicio1.dat", FileMode.OpenOrCreate, FileAccess.Write);

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
            #endregion
        }

        private void btnResguardar_Click(object sender, EventArgs e)
        {
            //resguardar o backup es similar al exportar - pero incluyo todo - no filtro ningun registro.

            saveFileDialog1.Title = "Backup del contexto";
            saveFileDialog1.Filter = "Fichero csv|*.csv";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;

                FileStream fs = null;
                StreamWriter sw = null;
                try
                {
                    fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                    sw = new StreamWriter(fs);

                    #region cabeceras
                    string linea = $"TIPO:BANCO|CLIENTE|CUENTA";
                    sw.Write(linea);
                    linea = $"CLIENTE;DNI;NOMBRE";
                    sw.Write(linea);
                    linea = $"CUENTA;NUMERO;SALDO;FECHA;DNI TITULAR";
                    sw.Write(linea);
                    #endregion

                    #region banco
                    linea = $"BANCO;";
                    sw.WriteLine(linea);
                    #endregion

                    #region clientes
                    for (int idx = 0; idx < banco.CantidadClientes; idx++)
                    {
                        Persona cliente = banco.VerCliente(idx);
                        linea = $"CLIENTE;{cliente.DNI};{cliente.Nombre}";
                        sw.WriteLine(linea);
                    }
                    #endregion

                    #region cuentas 
                    for (int idx = 0; idx < banco.CantidadCuentas; idx++)
                    {
                        Cuenta cuenta = banco.VerCuenta(idx);
                        linea = $"CUENTA;{cuenta.Numero};{cuenta.Saldo};{cuenta.Fecha:dd/MM/yyyy};{cuenta.Titular.DNI}";//el dni me sirve como referencia a la persona.
                        sw.WriteLine(linea);
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al exportar el contexto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (sr != null) sr.Close();
                    if (fs != null) sr.Close();
                }
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            //similar al importar - la diferencia es que ahora reemplazo al contexto anterior.

            Banco bancoImportado = null;
            List<Persona> personas = new List<Persona>();

            openFileDialog1.Title = "Restauración del contexto";
            openFileDialog1.Filter = "Fichero csv|*.csv";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;

                FileStream fs = null;
                StreamReader sr = null;
                try
                {
                    fs = new FileStream(path, FileMode.Open, FileAccess.Read);

                    sr = new StreamReader(fs);

                    //descarto las cabeceras
                    sr.ReadLine();
                    sr.ReadLine();
                    sr.ReadLine();

                    while (sr.EndOfStream == false)
                    {
                        string linea = sr.ReadLine();
                        string[] campos = linea.Split(';');

                        switch (campos[0])
                        {
                            case "BANCO":
                                {
                                    bancoImportado = new Banco();
                                }
                                break;
                            case "CLIENTE":
                                {
                                    int dni = Convert.ToInt32(campos[1]);
                                    string nombre = campos[2];
                                    personas.Add(new Persona(dni, nombre));

                                }
                                break;
                            case "CUENTA":
                                {
                                    int numero = Convert.ToInt32(campos[1]);
                                    double saldo = Convert.ToDouble(campos[2]);

                                    CultureInfo culture = CultureInfo.CurrentCulture;
                                    bool exito = DateTime.TryParseExact(campos[3], "dd/MM/yyyy", culture, DateTimeStyles.None, out DateTime fecha);
                                    if (exito == false) throw new Exception("Formato fecha inválido");

                                    #region cliente
                                    int dni = Convert.ToInt32(campos[3]);
                                    personas.Sort();//esto no es lo mejor, ordenar cada vez que que busco - pero para simplificar esta bien
                                    Persona cliente = new Persona(dni, "");
                                    int idx = personas.BinarySearch(cliente);
                                    if (idx > -1) cliente = personas[idx];
                                    else throw new Exception($"Cliente no encontrado {dni}");
                                    #endregion

                                    exito = banco.ImportarCuenta(numero, saldo, fecha, cliente);//necesito un metodo adicional por la estructura del modelo
                                    if (exito == false) throw new Exception($"No pudo importar la cuenta, linea: {linea}");

                                    //NOTA MUY IMPORTANTE!: Normalmente no lanzamos las excepciones en la misma capa,
                                    //lo mejor sería tener un metodo importar que reciba la lista de string y ahi haga el proceso
                                    //
                                    //el tema está en que si falla necesito interrumpir el proceso

                                }
                                break;
                        }

                    }

                    if (bancoImportado != null) banco = bancoImportado;
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
    }
}
