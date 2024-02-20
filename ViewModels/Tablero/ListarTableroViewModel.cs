using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ListarTableroViewModel
    {
        public List<TableroViewModel> TablerosViewModels {get;set;}
        public List<TableroViewModel> MyTablerosViewModels {get;set;}
        public string Operador {get; set;}
        public ListarTableroViewModel()
        {
            TablerosViewModels = new List<TableroViewModel>();
        }
        public ListarTableroViewModel(List<Tablero> tableros)
        {
            TablerosViewModels = new List<TableroViewModel>();
            foreach (var tablero in tableros)
            {
                var tableroViewModel = new TableroViewModel(tablero);
                TablerosViewModels.Add(tableroViewModel);  
            }
            MyTablerosViewModels = new List<TableroViewModel>();
            Operador = "";
        }  

        public ListarTableroViewModel(List<Tablero> tableros, List<Tablero> myTableros, string operador)
        {
            TablerosViewModels = new List<TableroViewModel>();
            MyTablerosViewModels = new List<TableroViewModel>();
            foreach (var tablero in tableros)
            {
                var tableroViewModel = new TableroViewModel(tablero);
                TablerosViewModels.Add(tableroViewModel);  
            }

            foreach (var tablero in myTableros)
            {
                var tableroViewModel = new TableroViewModel(tablero);
                MyTablerosViewModels.Add(tableroViewModel);  
            }
            Operador = operador;
          
        }       
    }
}