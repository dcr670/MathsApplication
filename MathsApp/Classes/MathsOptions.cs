using System.ComponentModel.DataAnnotations;

namespace MathsApp.Classes
{
    public class MathsOptions
    {
        public const string CalculationConfig = "CalculationConfig";

        [Required(AllowEmptyStrings = false)]
        public string SplitRegExPattern { get; set; }

        [Required(AllowEmptyStrings = false)]
        public bool UseFallBackService { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FallBackServiceURL { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FallBackServiceURLParameter { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string SplitBODMASRegExPattern { get; set; }
    }
}
