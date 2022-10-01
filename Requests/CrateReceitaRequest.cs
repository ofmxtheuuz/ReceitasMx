using System.ComponentModel.DataAnnotations;

namespace ReceitaMx.Models;

public class CrateReceitaRequest
{
    [Display(Name = "Titulo da receita")]
    public string Title { get; set; }
    [Display(Name = "Descrição da receita")]
    public string Description { get; set; }
    [Display(Name = "Passo a passo")]
    public string Guide { get; set; }
    [Display(Name = "Categoria da receita")]
    public int CategoriaId { get; set; }
}