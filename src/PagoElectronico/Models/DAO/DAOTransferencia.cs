using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagoElectronico.Models.BO;

namespace PagoElectronico.Models.DAO
{
    public partial class DAOTransferencia : DAOBase<Transferencia>
    {
         public DAOTransferencia()
            : base("VIDA_ESTATICA.Transferencia", "id") {
        }
    }
}
