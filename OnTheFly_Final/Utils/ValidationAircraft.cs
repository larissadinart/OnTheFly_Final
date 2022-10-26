using OnTheFly_Final.Models;
using OnTheFly_Final.Services;
using System;
using System.Linq;

namespace OnTheFly_Final.Utils
{
    public class ValidationAircraft
    {
        public string RabValidation(string rab)
        {
            string[] prefixAircraft = new string[] { "PU","pu", "PT","pt", "PS","ps", "PR","pr", "PP","pp" };

            string[] rabForbidden = new string[] { "SOS","sos", "XXX","xxx", "PAN","pan", "TTT","ttt", "VFR","vfr", "IFR","ifr", "VMC","vmc", "IMC","imc" };

            char[] letters = rab.ToCharArray();
            //verifica o tamanho do rab
            if (letters.Length == 5)
            {
                //verifica se tem q e w onde não pode ter
                if (letters[3] != 'Q' && letters[4] != 'W'|| letters[3] != 'q' && letters[4] != 'w')
                {
                    string brazilianAeronauticalRegistration = letters[2].ToString()+ letters[3].ToString() + letters[4].ToString();
                    if (rabForbidden.Contains(brazilianAeronauticalRegistration) == false)
                    {
                        string prefixRab = letters[0].ToString() + letters[1].ToString();
                        if (prefixAircraft.Contains(prefixRab) == true)
                        {
                            //chamo o método da GabiCiriano para validar entrada do cnpj

                            return rab;
                        }
                        else
                        {
                            return "Prefixos devem ser PU,PT,PS,PR,PP";
                          //  return null;
                        }
                    }
                    else
                    {
                        return "SOS, XXX, PAN, TTT, VFR, IFR, VMC e IMC não podem ser utilizadas";
                       //return null;
                    }
                }
                else
                {
                    return "A letra Q como primeira letra e nem a letra W como segunda letra da matrícula da aeronave não são permitidas";
                   // return null;
                }
            }
            else
            {
                return "Quantidade incorreta de dígitos de identificação";
               // return null;
            }
        }
    }
}