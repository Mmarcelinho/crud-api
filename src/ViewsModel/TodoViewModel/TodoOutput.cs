namespace AspNetCore.WebApi.ViewsModel.TodoViewModel;

public class TodoOutput
{

    public TodoOutput(int id, string title, DateTime date, bool done)
    {
        this.Id = id;
        this.Title = title;
        this.Date = date;
        this.Done = done;

    }
    public int Id { get; set; }
    public string Title { get; set; }

    public DateTime Date { get; set; }

    public bool Done { get; set; }
}
