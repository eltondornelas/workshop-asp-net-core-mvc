using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel
    {
        //esse m�todo n�o � exatamente um model, uma entidade do neg�cio, ele � um modelo auxiliar para povoar as telas
        //por conta disso vamos mudar de pasta e vamos deixar em model apenas as classes models mesmo que s�o Seller, SalesRecord e Department
        public string RequestId { get; set; }
        public string Message { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    //como mudou o namespace o arquivo vai quebrar, com isso pressiona ctrl+shift+b ou vai em build->build SalesWebMvc
}