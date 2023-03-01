namespace AspNetCore.WebApi.ViewsModel.TodoViewModel;
public class TodoInput
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo Obrigátorio")]
    [MinLength(3, ErrorMessage = "Minimo de 3 caracteres")]
    public string Title { get; set; }


}
