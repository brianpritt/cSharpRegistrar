using Nancy;
using System.Collections.Generic;
using System;
using Registrar.Objects;

namespace Registrar
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["TEMPLATE.cshtml"];
      };

    }
  }
}
