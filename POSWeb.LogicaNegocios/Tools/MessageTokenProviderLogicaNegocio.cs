using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSWeb.Entidades;
using POSWeb.AccesoDatos;

namespace POSWeb.LogicaNegocios
{
    public class MessageTokenProviderLogicaNegocio
    {

        /// <summary>
        /// Metodo que sirve para reemplazar los Token del Usuario
        /// </summary>
        /// <param name="pCatalogo">Filtro de tipo Catalogo</param>
        /// <returns></returns>
        public List<Token> AddUsuarioTokens(List<Token> tokens, Usuario pUsuario)
        {
            if (pUsuario == null)
                throw new ArgumentNullException("usuario");

            UsuarioLogicaNegocio bl = new UsuarioLogicaNegocio();
            Usuario vUsuario = bl.ObtenerUsuario(new Usuario() { CodigoUsuario = pUsuario.CodigoUsuario }).Usuario;

            tokens.Add(new Token("%Usuario.Nombre%", vUsuario.Nombre));
            //tokens.Add(new Token("%Usuario.Email%", usuario.Email));
            tokens.Add(new Token("%Usuario.CodigoUsuario%", vUsuario.CodigoUsuario));
            tokens.Add(new Token("%Usuario.Identificacion%", vUsuario.Identificacion));
            tokens.Add(new Token("%Usuario.Password%", vUsuario.Clave));
            return tokens;
            //event notification
            //_eventPublisher.EntityTokensAdded(store, tokens);
        }

        /// <summary>
        /// Metodo que sirve para reemplazar los Token de Parámetros
        /// </summary>
        /// <param name="pCatalogo">Filtro de tipo Catalogo</param>
        /// <returns></returns>
        public List<Token> AddParametroTokens(List<Token> tokens, long CodigoEntidad)
        {
            List<String> mensajes = new List<string>();
            CatalogoLogicaNegocio bl = new CatalogoLogicaNegocio();
            if (CodigoEntidad == 0)
                throw new ArgumentNullException("CodigoEntidad");
            tokens.Add(new Token("%Parametro.HorasPassword%", bl.ObtenerCatalogo(new Catalogo { CodigoParametro = "HorasPassword" }).Catalogo.Descripcion));
            return tokens;
            //event notification
            //_eventPublisher.EntityTokensAdded(store, tokens);
        }

        /// <summary>
        /// Metodo que sirve para obtener todos los token disponibles
        /// </summary>
        /// <param name="pCatalogo">Filtro de tipo Catalogo</param>
        /// <returns></returns>
        public virtual string[] GetListOfNotificacionesAllowedTokens()
        {
            //var additionTokens = new CampaignAdditionTokensAddedEvent();
            //_eventPublisher.Publish(additionTokens);

            var allowedTokens = new List<string>
            {
                "%Parametro.HorasPassword%",
                "%Usuario.Nombre%",
                "%Usuario.CodigoUsuario%",
                "%Usuario.Identificacion%",
                "%Usuario.Password%",
            };
            //allowedTokens.AddRange(additionTokens.AdditionTokens);
            return allowedTokens.Distinct().ToArray();
        }
    }
}
