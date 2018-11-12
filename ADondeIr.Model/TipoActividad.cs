﻿namespace ADondeIr.Model
{
    using FluentValidation;
    using FluentValidation.Attributes;
    using NPoco;

    [PrimaryKey("pkTipoActividad")]
    [Validator(typeof(TipoActividadValidator))]
    public class TipoActividad : BaseEntity
    {
        public int pkTipoActividad { get; set; }

        public string cTipoActividad { get; set; }
    }

    public class TipoActividadValidator : AbstractValidator<TipoActividad>
    {
        public TipoActividadValidator()
        {
            RuleFor(x => x.cTipoActividad).NotEmpty().WithMessage("Ingrese un nombre para la Tipo de Actividad!!!")
                .Matches(@"^(?=.*\S).*$").WithMessage("Ingrese un nombre de Tipo de Actividad valida!!!");
        }
    }
}