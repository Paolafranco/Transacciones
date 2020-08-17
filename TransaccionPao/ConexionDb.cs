using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransaccionPao
{
    class ConexionDb
    {
        NpgsqlConnection conexionDb = new NpgsqlConnection("Server = localhost; User Id = postgres; Password = 1234; Database = Db_estudiantes;");

        public DataTable Consultar()
        {
            string query = "SELECT  *  FROM clientes";
            NpgsqlCommand conector = new NpgsqlCommand(query, conexionDb);
            NpgsqlDataAdapter datos = new NpgsqlDataAdapter(conector);
            DataTable tabla = new DataTable();
            datos.Fill(tabla);
            return tabla;
        }
        
            public void Transferir(string textBox1, string textBox2, int textBox3)
            {
                try
                {
                conexionDb.Open();
                    string query1 = "BEGIN";
                    NpgsqlCommand ejecutor1 = new NpgsqlCommand(query1, conexionDb);
                    ejecutor1.ExecuteNonQuery();

                    string query2 = $"UPDATE clientes set saldos = saldos - {textBox3} where cuenta = '{textBox2}' and saldos >= {textBox3}";
                     NpgsqlCommand ejecutor2 = new NpgsqlCommand(query2, conexionDb);
                    var e = ejecutor2.ExecuteNonQuery();

                    string query3 = $"UPDATE clientes set saldos=saldos+ {textBox3} where cuenta = '{textBox1}'";
                    NpgsqlCommand ejecutor3 = new NpgsqlCommand(query3, conexionDb);
                    var f = ejecutor3.ExecuteNonQuery();

                    if (f == 1 && e == 1)
                    {
                        string query4 = "COMMIT";
                        NpgsqlCommand ejecutor4 = new NpgsqlCommand(query4, conexionDb);
                    ejecutor4.ExecuteNonQuery();
                    conexionDb.Close();
                        MessageBox.Show("Transacción realizada con éxito.");
                    }
                    else
                    {
                        string query4 = "ROLLBACK";
                        NpgsqlCommand ejecutor4 = new NpgsqlCommand(query4, conexionDb);
                        ejecutor4.ExecuteNonQuery();
                    conexionDb.Close();
                        MessageBox.Show("La transacción no pudo ser realizada.");
                    }
                }
                catch (Exception)
                {
                conexionDb.Close();
                    MessageBox.Show("La transacción no pudo ser realizada.");
                }

            }
        }
    }

