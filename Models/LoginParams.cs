using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HumanResources.Models
{

    #region "Opis"
    /*
     *  Obiekt do przekazania wielu parametrów 
     *  współpracuje z converterem LoginParamsConverter
     *  Musimy dodać taką klasę i konwerter, ponieważ chcemy przekazać po kliknięciu 2 parametry.
     *  Jeden do informacje o oknie, czyli to co miałeś wcześniej,
     *  ale też musimy przekazać informacje o passwordbox i później będziemy mogli wyłuskać z tej kontrolki wpisane hasło.
     */
    #endregion

    public class LoginParams
    {
        public Window Window { get; set; }
        public PasswordBox PasswordBox { get; set; }
    }

}
