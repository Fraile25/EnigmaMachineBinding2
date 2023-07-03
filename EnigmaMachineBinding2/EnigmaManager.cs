using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binding3
{
    public class EnigmaManager
    {
        public char[] abcdef = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '.', '_', '!', '¡', '¿', '?', ';' };
        public string[] rotor1 = new string[] { "ad", "bh", "cn", "dm", "ec", "fw", "gt", "hv", "ix", "ju", "ki", "ly", "m1", "nb", "or", "p0", "qs", "rg", "s2", "tq", "u7", "v6", "wp", "x8", "y5", "z;", "0z", "19", "2¡", "3a", "4?", "5,", "64", "7¿", "8f", "9!", ",k", "._", "_j", "!3", "¡o", "¿l", "?e", ";." };
        public string[] rotor2 = new string[] { "ah", "bc", "cy", "dq", "e3", "f2", "gi", "hr", "i4", "j1", "kb", "lj", "mp", "ng", "oo", "p6", "q7", "r5", "sn", "t?", "u8", "vf", "w_", "x.", "y9", "z;", "0¿", "1!", "2,", "3¡", "4e", "5u", "6m", "7v", "8d", "9x", ",w", ".l", "_t", "!0", "¡s", "¿z", "?k", ";a" };
        public string[] rotor3 = new string[] { "ao", "bz", "cp", "dn", "e2", "f5", "g1", "h,", "if", "j4", "ky", "l_", "m9", "ne", "o.", "p3", "qd", "rj", "si", "t8", "um", "v0", "wl", "x7", "y6", "z!", "0k", "1?", "2s", "3t", "4g", "5;", "6¿", "7r", "8¡", "9a", ",u", ".v", "_b", "!x", "¡h", "¿w", "?q", ";c" };
        public char[] reflector = new char[] { 'd', 'f', 'c', 'f', 'j', 'i', 'h', 'b', 'r', 'p', 'm', 'i', 'n', 'l', 'e', 's', 's', 'c', 't', 'u', 'a', 'v', 'o', 'v', 'u', 'e', 't', 'r', 'q', 'q', 'p', 'b', 'h', 'g', 'k', 'm', 'd', 'o', 'k', 'n', 'l', 'j', 'a', 'g' };


        public int contador1 = 0;
        public int contador2 = 0;
        public int contador3 = 0;


        public static string RemoverAcentos(string cadena)
        {
            string cadenaNormalizada = cadena.Normalize(NormalizationForm.FormD);
            StringBuilder cadenaSinAcentos = new StringBuilder();

            foreach (char caracter in cadenaNormalizada)
            {
                UnicodeCategory categoria = CharUnicodeInfo.GetUnicodeCategory(caracter);
                if (categoria != UnicodeCategory.NonSpacingMark)
                {
                    cadenaSinAcentos.Append(caracter);
                }
            }

            return cadenaSinAcentos.ToString();
        }

        public string[] RotarRotor(string[] src, int by)
        {
            string[] output = new string[src.Length]; int n = by;

            if (by < 0)
                by = src.Length - by;

            for (int i = 0; i < src.Length; i++)
            {
                output[n] = src[i];
                if (n + 1 >= src.Length)
                    n = 0;
                else if (n < 0)
                    n = by + src.Length;
                else
                    n++;
            }
            return output;
        }

        public int BuscarEnReflector(char letra, int pos)
        {
            int primeraCohincidencia = -1;

            for (int i = 0; i < reflector.Length; i++)
            {
                if (i > reflector.Length)
                {
                    i = i % reflector.Length;
                }
                else
                {
                    if (reflector[i] == letra && i != pos)
                    {
                        return i;
                    }
                    else
                    {
                        if (primeraCohincidencia != -1)
                        {
                            primeraCohincidencia = i;
                        }
                    }
                }
            }
            return primeraCohincidencia;
        }


        public string[] ColocarClaveRotor(string[] rotor, char key)
        {
            int i = 0;
            while (rotor[i][0] != key)
            {

                rotor = RotarRotor(rotor, 1);
                i++;
                if (i >= rotor.Length - 1)
                {
                    i = 0;
                }

            }


            return rotor;
        }

        public void ColocarClaveRotores(string keys)
        {
            rotor1 = ColocarClaveRotor(rotor1, keys[0]);
            rotor2 = ColocarClaveRotor(rotor2, keys[1]);
            rotor3 = ColocarClaveRotor(rotor3, keys[2]);
        }

        public int IndiceLetraAbecedario(char letra)
        {
            int i = 0;
            char letra2 = ' ';

            if (Array.IndexOf(abcdef, letra) == -1)
            {
                letra = 'x';
            }

            while (abcdef[i] != letra)
            {

                letra2 = abcdef[i];
                i++;
            }

            return i;
        }

        public int IndiceLetraRotor(char letra, string[] r)
        {
            int i = 0;
            char letra2 = ' ';
            while (r[i][1] != letra)
            {
                letra2 = r[i][1];
                i++;
            }

            return i;
        }

        public char BuscarFueraAbecedario_Normal(char letra, string[] r)
        {
            int index = IndiceLetraAbecedario(letra);
            char letra2 = r[index][0];

            return letra2;
        }

        public char BuscarFueraAbecedario_Invertido(char letra, string[] r)
        {
            char letra2 = r[IndiceLetraRotor(letra, r)][0];
            int index = IndiceLetraAbecedario(letra2);

            return letra2;
        }

        public char ObtenerLetraFueraRotor_Normal(string[] r1, string[] r2, int posicion)
        {
            char aux = r1[posicion][1];
            char letra = r2[posicion][0];

            return letra;
        }

        public char ObtenerLetraFueraRotor_Invertido(string[] r1, string[] r2, int posicion)
        {

            char aux = r2[posicion][0];
            char letra = r1[posicion][1];

            return letra;
        }

        public int BuscarIndiceLetraDentroRotor_Normal(string[] r, char letra, int posicion)
        {
            char aux = r[posicion][0];
            char aux2 = ' ';
            int i = 0;
            while (r[i][1] != letra)
            {
                aux2 = r[i][1];

                i++;

                if (i >= r.Length)
                {
                    i = i % r.Length;
                }

            }
            return i;
        }

        public int BuscarIndiceLetraDentroRotor_Invertido(string[] r, char letra, int posicion)
        {
            char aux = r[posicion][1];
            char aux2 = ' ';
            int i = 0;
            while (r[i][0] != letra)
            {
                aux2 = r[i][0];

                i++;

                if (i >= r.Length)
                {
                    i = i % r.Length;
                }

            }
            return i;
        }

        public static string TrimSupremo(string str)
        {
            string new_str = "";
            if (str == null)
            {
                str = " ";
            }
            str = str.ToLower();
            str = RemoverAcentos(str);
            new_str = str.Replace(" ", "_");

            return new_str;
        }

        public char ProcesoEnigma(char letra)
        {
            char letra_r1 = ' ';
            char letra_r2 = ' ';
            char letra_r3 = ' ';
            char letra_r1_1 = ' ';
            char letra_r2_1 = ' ';
            char letra_r3_1 = ' ';
            char letra_reflector = ' ';
            char letra_reflector_1 = ' ';

            int indice_letraAbcd = 0;
            int indice_letra_r1_0 = 0;
            int indice_letra_r2_0 = 0;
            int indice_letra_r3_0 = 0;
            int indice_letra_r1_1 = 0;
            int indice_letra_r2_1 = 0;
            int indice_letra_r3_1 = 0;
            int indice_letra_reflector = 0;
            int indice_letra_reflector_1 = 0;



            indice_letraAbcd = IndiceLetraAbecedario(letra);

            letra_r1 = rotor1[indice_letraAbcd][0];
            indice_letra_r1_0 = BuscarIndiceLetraDentroRotor_Normal(rotor1, letra_r1, indice_letraAbcd);

            letra_r2 = rotor2[indice_letra_r1_0][0];
            indice_letra_r2_0 = BuscarIndiceLetraDentroRotor_Normal(rotor2, letra_r2, indice_letra_r1_0);

            letra_r3 = rotor3[indice_letra_r2_0][0];
            indice_letra_r3_0 = BuscarIndiceLetraDentroRotor_Normal(rotor3, letra_r3, indice_letra_r2_0);

            letra_reflector = reflector[indice_letra_r3_0];
            indice_letra_reflector_1 = BuscarEnReflector(letra_reflector, indice_letra_r3_0);

            letra_r3_1 = rotor3[indice_letra_reflector_1][1];
            indice_letra_r3_1 = BuscarIndiceLetraDentroRotor_Invertido(rotor3, letra_r3_1, indice_letra_reflector_1);

            letra_r2_1 = rotor2[indice_letra_r3_1][1];
            indice_letra_r2_1 = BuscarIndiceLetraDentroRotor_Invertido(rotor2, letra_r2_1, indice_letra_r2_1);

            letra_r1_1 = rotor1[indice_letra_r2_1][1];
            indice_letra_r1_1 = BuscarIndiceLetraDentroRotor_Invertido(rotor1, letra_r1_1, indice_letra_r1_1);



            rotor1 = RotarRotor(rotor1, 1);

            return abcdef[indice_letra_r1_1];
        }

        public string[] InicializarRotor(string[] r)
        {
            int i = 0;

            while (r[0][0] != 'a')
            {
                r = RotarRotor(r, 1);
                i++;
                if (i > r.Length - 1)
                {
                    i = 0;
                }
            }

            return r;
        }

        public void InicializarRotores()
        {
            rotor1 = InicializarRotor(rotor1);
            rotor2 = InicializarRotor(rotor2);
            rotor3 = InicializarRotor(rotor3);
        }

        public string MiCifradorEnigma(string msg, string keys)
        {
            string msg_cif = "";
            char[] claves = new char[3];

            claves[0] = keys[0];
            claves[1] = keys[1];
            claves[2] = keys[2];

            if (keys[0]==null)
            {
                claves[0] = 'a';
            }
            else if (keys[1]==null)
            {
                claves[1] = 'a';
            }
            else if (keys[2]==null)
            {
                claves[2] = 'a';
            }
            




            int cont = 0;


            msg = TrimSupremo(msg);
            int lonMsg = msg.Length;


            rotor1 = ColocarClaveRotor(rotor1, claves[0]);
            rotor2 = ColocarClaveRotor(rotor2, claves[1]);
            rotor3 = ColocarClaveRotor(rotor3, claves[2]);


            for (int i = 0; i < lonMsg; i++)
            {
                if (contador1 > rotor1.Length)
                {
                    contador1 = 0;
                    cont++;
                    contador2 = cont;
                    msg_cif += ProcesoEnigma(msg[i]);
                    rotor2 = RotarRotor(rotor2, 1);
                }
                else if (contador2 > rotor1.Length)
                {
                    contador2 = 0;
                    cont++;
                    contador3 = cont;
                    msg_cif += ProcesoEnigma(msg[i]);
                    rotor3 = RotarRotor(rotor3, 1);
                }
                else if (contador3 > rotor1.Length)
                {
                    contador1 = 0;
                    contador2 = 0;
                    contador3 = cont;
                    msg_cif += ProcesoEnigma(msg[i]);
                    rotor1 = RotarRotor(rotor1, 1);
                }
                else
                {
                    if (contador1 < rotor1.Length)
                    {
                        contador1 = cont;
                        msg_cif += ProcesoEnigma(msg[i]);
                        rotor1 = RotarRotor(rotor1, 1);
                        cont++;
                    }
                    else if (contador2 < rotor1.Length)
                    {
                        contador2 = cont;
                        msg_cif += ProcesoEnigma(msg[i]);
                        rotor2 = RotarRotor(rotor2, 1);
                        cont++;
                    }
                    else if (contador3 < rotor1.Length)
                    {
                        contador3 = cont;
                        msg_cif += ProcesoEnigma(msg[i]);
                        rotor3 = RotarRotor(rotor3, 1);
                        cont++;
                    }
                    else
                    {
                        contador1 = 0;
                        contador2 = 0;
                        contador3 = 0;
                        msg_cif += ProcesoEnigma(msg[i]);
                        rotor1 = RotarRotor(rotor1, 1);
                        rotor2 = RotarRotor(rotor2, 1);
                        rotor3 = RotarRotor(rotor3, 1);
                    }

                }


            }
            rotor1 = InicializarRotor(rotor1);
            rotor2 = InicializarRotor(rotor2);
            rotor3 = InicializarRotor(rotor3);

            //InicializarRotores();
            return msg_cif.ToUpper();
        }
    }
}
