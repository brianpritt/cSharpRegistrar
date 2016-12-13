using Xunit;
using System;
using System.Collections.Generic;
using Registrar.Objects;

namespace  Registrar
{
  public class TEMPLATE
  {
    public R()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=todo_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void TEMPLATE_true()
    {
      //Arrange
      //Act
      //Assert
      Assert.Equal(true/false, TEMPLATE);
    }
  }
}
