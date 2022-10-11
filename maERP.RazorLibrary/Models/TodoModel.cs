using System;

namespace maERP.RazorLibrary.Models;

public class TodoModel
{
	public int Id { get; set; }
	public string TodoItem { get; set; }
	public bool isComplete { get; set; }
}
