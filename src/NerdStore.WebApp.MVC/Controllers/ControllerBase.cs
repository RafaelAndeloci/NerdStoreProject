using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApp.MVC.Controllers;

public abstract class ControllerBase : Controller
{
    protected Guid ClientId = Guid.Parse("4885e415-b0e4-4490-b959-04fabc806d32");
}