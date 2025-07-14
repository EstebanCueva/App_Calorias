using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Calorias.Models
{
    public class UsuarioSimple
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string Activity { get; set; }
        public string Sex { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int TotalCalories { get; set; }
        public int IdDieta { get; set; }
    }
}
