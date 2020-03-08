using RegistroDetails.BLL;
using RegistroDetails.Entidades;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RegistroDetails.UI.Registros
{
    /// <summary>
    /// Interaction logic for rPersonas.xaml
    /// </summary>
    public partial class rPersonas : Window
    {
        public List<TelefonoDetalles> Detalles { get; set; }
        public rPersonas()
        {
            InitializeComponent();
            Detalles = new List<TelefonoDetalles>();
        }

        private void Limpiar()
        {
            IdTextBox.Text = "0";
            NombreTextBox.Text = string.Empty;
            TelefonoTextBox.Text = string.Empty;
            CedulaTextBox.Text = string.Empty;
            DireccionTextBox.Text = string.Empty;
            FechaDatePicker.SelectedDate = DateTime.Now;

            Detalles = new List<TelefonoDetalles>();

            CargarGrid();

        }

        private Persona LlenaClase()
        {
            Persona personas = new Persona();
            personas.PersonaId = Convert.ToInt32(IdTextBox.Text);

            //idTextBox.Text.ToInt();
            personas.Nombre = NombreTextBox.Text;

            personas.Cedula = CedulaTextBox.Text;
            personas.Direccion = DireccionTextBox.Text;
            personas.FechaNacimiento = (DateTime)FechaDatePicker.SelectedDate;
            personas.Telefonos = Detalles;

            return personas;
        }

        private void LlenaCampo(Persona personas)
        {
            IdTextBox.Text = Convert.ToString(personas.PersonaId);
            NombreTextBox.Text = personas.Nombre;
            CedulaTextBox.Text = personas.Cedula;
            DireccionTextBox.Text = personas.Direccion;
            FechaDatePicker.SelectedDate = personas.FechaNacimiento;

           Detalles = personas.Telefonos;
            CargarGrid();
        }

        private bool Validar()
        {
            bool paso = true;

            if (NombreTextBox.Text == string.Empty)
            {
                MessageBox.Show(NombreTextBox.Text, "El campo Nombre no puede estar vacio ");
                NombreTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(DireccionTextBox.Text))
            {
                MessageBox.Show(DireccionTextBox.Text, "El campo Direccion no puede estar vacio");
                DireccionTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(CedulaTextBox.Text.Replace("-", "")))
            {
                MessageBox.Show(CedulaTextBox.Text, "El campo Cedula no puede estar vacio");
                CedulaTextBox.Focus();
                paso = false;
            }


            if (this.Detalles.Count == 0)
            {
                MessageBox.Show(DetalleDataGrid.ToString(), "Debe Agregar algun Telefono");
                TelefonoTextBox.Focus();
                paso = false;
            }

            return paso;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Persona personas = PersonaBLL.Buscar((int)IdTextBox.Text.ToInt());
            return (personas != null);
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void ButtonAgreg_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.ItemsSource != null)
                this.Detalles = (List<TelefonoDetalles>)DetalleDataGrid.ItemsSource;
            //todo: validar campos del detalle

            //Agregar un nuevo detalle con los datos introducidos.
            Detalles.Add(new TelefonoDetalles 
            {
             
                TipoTelefono= TipoTextBox.Text,

                Telefono = TelefonoTextBox.Text,
                     /*id: 0,
                     //idPersona: (int)IdTextBox.Text.ToInt(),
                     telefono: TelefonoTextBox.Text,
                     tipoTelefono: TipoTextBox.Text*/
                     
                });

            CargarGrid();
            TelefonoTextBox.Focus();
            TelefonoTextBox.Clear();
            TipoTextBox.Clear();
          
        }

        private void ButtonRemo_Click(object sender, RoutedEventArgs e)
        {


            if (DetalleDataGrid.Items.Count > 0 && DetalleDataGrid.SelectedItem != null)
            {
                //remover la fila
                Detalles.RemoveAt(DetalleDataGrid.SelectedIndex);

                //    SelectedItem
                CargarGrid();
            }

            //DetalleDataGrid.Items.RemoveAt(DetalleDataGrid.SelectedIndex);
        }

        private void CargarGrid()
        {
            DetalleDataGrid.ItemsSource = null;
            DetalleDataGrid.ItemsSource = Detalles;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Persona personas;
            bool paso = false;

            if (!Validar())
                return;

            personas = LlenaClase();
            Limpiar();

            //Determinar si es guardar o modificar
            if (IdTextBox.Text == "0")
                paso = PersonaBLL.Guardar(personas);

            if (string.IsNullOrWhiteSpace(IdTextBox.Text) || IdTextBox.Text == "0")
                paso = PersonaBLL.Guardar(personas);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar una persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = PersonaBLL.Modificar(personas);
            }

            //Informar el resultado
            if (paso)
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Persona persona = new Persona();
            int.TryParse(IdTextBox.Text, out id);

            Limpiar();

            persona = PersonaBLL.Buscar(id);

            if (persona != null)
            {
                MessageBox.Show("Persona Encontrada");
                LlenaCampo(persona);
            }

            else
            {
                MessageBox.Show("Persona no Encontrada");
            }
        }

        private void ButtonEliminar_Click(object sender, RoutedEventArgs e)
        {

            int id;
            int.TryParse(IdTextBox.Text, out id);

            Limpiar();

            if (PersonaBLL.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(IdTextBox.Text, "No se puede eliminar una persona que no existe");
        }
    }
}
