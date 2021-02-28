using System.ComponentModel.DataAnnotations;

namespace MathsApp.Classes
{
    public class MathsOptions
    {
        public const string CalculationConfig = "CalculationConfig";

        [Required]
        public string SplitRegExPattern { get; set; }

        [Required]
        public bool UseFallBackService { get; set; }

        [Required]
        public string FallBackServiceURL { get; set; }

        [Required]
        public string FallBackServiceURLParameter { get; set; }

        [Required]
        public string SplitBODMASRegExPattern { get; set; }
    }
}
