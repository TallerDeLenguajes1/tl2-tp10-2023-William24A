using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ListarTableroViewModel
    {
        public List<TableroViewModel> TablerosViewModels {get;set;}
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
          
        }        
    }
}