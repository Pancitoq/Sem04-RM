using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Lab04_RM
{
    public partial class MainWindow : Window
    {
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarProductos();
            CargarCategorias();
        }

        private void btnListarProductos_Click(object sender, RoutedEventArgs e)
        {
            CargarProductos();
        }

        private void btnListarCategorias_Click(object sender, RoutedEventArgs e)
        {
            CargarCategorias();
        }

        private void CargarProductos()
        {
            try
            {
                string cadena = "Server=LAB1507-09\\SQLEXPRESS02; Database=NeptunoB; Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("USP_ListarProductos", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    List<Producto> listaProductos = new List<Producto>();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto producto = new Producto
                        {
                            IdProducto = reader["idproducto"].ToString(),
                            NombreProducto = reader["nombreProducto"].ToString(),
                            IdProovedor = reader["idProveedor"].ToString(),
                            IdCategoria = reader["idCategoria"].ToString(),
                            CantidadPorUnidad = reader["cantidadPorUnidad"].ToString()
                        };
                        listaProductos.Add(producto);
                    }

                    dgvProductos.ItemsSource = listaProductos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void CargarCategorias()
        {
            try
            {
                string cadena = "Server=LAB1507-09\\SQLEXPRESS02; Database=NeptunoB; Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("USP_ListarCategorias", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    List<Categoria> listaCategoria = new List<Categoria>();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Categoria categoria = new Categoria
                        {
                            IdCategoria = reader["idcategoria"].ToString(),
                            NombreCategoria = reader["nombrecategoria"].ToString(),
                            Descripcion = reader["descripcion"].ToString(),
                            Activo = reader["Activo"].ToString(),
                            CodCategoria = reader["CodCategoria"].ToString()
                        };
                        listaCategoria.Add(categoria);
                    }

                    dgvCategorias.ItemsSource = listaCategoria;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }



        public class Producto
        {
            public string IdProducto { get; set; }
            public string NombreProducto { get; set; }
            public string IdProovedor { get; set; }
            public string IdCategoria { get; set; }
            public string CantidadPorUnidad { get; set; }
        }

        public class Categoria
        {
            public string IdCategoria { get; set; }
            public string NombreCategoria { get; set; }
            public string Descripcion { get; set; }
            public string Activo { get; set; }
            public string CodCategoria { get; set; }
        }
    }
}