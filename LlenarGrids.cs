using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using LibConexionBD;

namespace LibLlenarGrids
{
    public class LlenarGrids
    {
        #region "Atributos"
        private string archivoParametros;
        private string sql;
        private string error;
        #endregion

        #region "Propiedades"
        public string SQL
        {
            set { sql = value; }
        }

        public string Error
        {
            get { return error; }
        }
        #endregion

        #region "Constructor"
        public LlenarGrids(string archivoParametros)
        {
            this.archivoParametros = archivoParametros;
            sql = string.Empty;
            error = string.Empty;
        }
        #endregion

        #region "Métodos Privados"
        private bool Validar()
        {
            if (string.IsNullOrEmpty(sql))
            {
                error = "Debe definir la instruccion SQL";
                return false;
            }

            return true;
        }
        #endregion

        #region "Métodos Públicos"
        public bool LlenarGridWindows(DataGridView Generico)
        {
            if (!Validar())
            {
                return false;
            }

            ConexionBD conexion = new ConexionBD(archivoParametros);
            conexion.SQL = sql;

            if (!conexion.LLenarDataSet(false))
            {
                error = conexion.Error;
                conexion.CerrarConexion();
                conexion = null;
                return false;
            }

            Generico.DataSource = conexion.Ds.Tables[0];
            Generico.Refresh();
            conexion.CerrarConexion();
            conexion = null;
            return true;
        }

        public bool LlenarGridWeb(GridView Generico)
        {
            if (!Validar())
            {
                return false;
            }

            ConexionBD conexion = new ConexionBD(archivoParametros);
            conexion.SQL = sql;

            if (!conexion.LLenarDataSet(false))
            {
                error = conexion.Error;
                conexion.CerrarConexion();
                conexion = null;
                return false;
            }

            Generico.DataSource = conexion.Ds.Tables[0];
            Generico.DataBind();
            conexion.CerrarConexion();
            conexion = null;
            return true;
        }
        #endregion
    }
}
