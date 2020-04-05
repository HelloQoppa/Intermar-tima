using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Intermaritima.ApplicationCore.Entity
{
    public class Estabelecimento
    {
        public Estabelecimento()
        {

        }
        [Required]
        [Range(3, 3)]
        public virtual Char IdEstabelecimento { get; set; }//  cod-estabel
        [Required]
        [Range(8, 8)]
        public virtual Char ContaContabil { get; set; }//    ct-codigo
        [Required]
        [Range(8, 8)]
        public virtual Char CentroDeCusto { get; set; }//    centro-custo
        [Required]
        [Range(60, 60)]
        public virtual Char HistoricoDoLancamento { get; set; }//    historico
        [Required]
        public virtual DateTime DataEmissao { get; set; }//    dt-emissao
        [Required]
        [Range(5, 5)]
        public virtual Char Serie { get; set; }//    serie-docto
        [Required]
        [Range(16, 16)]
        public virtual Char Especie { get; set; }//    cod-esp
        [Required]
        [Range(16, 16)]
        public virtual Char Titulo { get; set; }//    nro-docto
        [Required]
        [Range(6, 6)]
        public virtual Char Cfop { get; set; }//    nat-operacao
        [Required]
        public virtual int Emitente { get; set; }//    cod-emitente
        [Required]
        [Range(80, 80)]
        public virtual Char Nome { get; set; }//    nome-fornec
        [Required]
        public virtual int Requisicao { get; set; }//    nr-requisicao
        [Required]
        public virtual int Seq { get; set; }//    sequencia
        [Required]
        [Range(16, 16)]
        public virtual Char NumNF { get; set; }//    nr-nota
        [Required]
        [Range(16, 16)]
        public virtual Char CodItem { get; set; }//    it-codigo
        [Required]
        [Range(3, 3)]
        public virtual Decimal ValorDebito { get; set; }//    vl-debito
        [Required]
        public virtual Decimal ValorCredito { get; set; }//    vl-credito
        [Required]
        public virtual Decimal Quantidade { get; set; }//    qt-requisitada

               
    }
}
