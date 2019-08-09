using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel
    {
        //esse método não é exatamente um model, uma entidade do negócio, ele é um modelo auxiliar para povoar as telas
        //por conta disso vamos mudar de pasta e vamos deixar em model apenas as classes models mesmo que são Seller, SalesRecord e Department
        public string RequestId { get; set; }
        public string Message { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    //como mudou o namespace o arquivo vai quebrar, com isso pressiona ctrl+shift+b ou vai em build->build SalesWebMvc
}