using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.DTOs;

public class CategoryDto
{
    public CategoryDto()
    {
        Id = Guid.NewGuid();
    }
    
    [Key] 
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int Code { get; set; }
}