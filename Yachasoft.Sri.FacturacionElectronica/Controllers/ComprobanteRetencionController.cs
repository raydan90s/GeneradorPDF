public async Task<IActionResult> GenerarRetencion()
{
    // 1️⃣ Crear el comprobante de retención
    var retencion = new ComprobanteRetencion_1_0_0Modelo.ComprobanteRetencion
    {
        PuntoEmision = ...,
        FechaEmision = DateTime.Now,
        InfoCompRetencion = new ComprobanteRetencion_1_0_0Modelo.InfoCompRetencion
        {
            PeriodoFiscal = "10/2025"
        },
        Sujeto = ...,
        Impuestos = new List<ComprobanteRetencion_1_0_0Modelo.ImpuestoRetencion>
        {
            new ComprobanteRetencion_1_0_0Modelo.ImpuestoRenta
            {
                BaseImponible = 100,
                Tarifa = 10,
                Valor = 10,
                CodigoRetencion = EnumTipoRetencionRenta._10,
                DocumentoSustento = new DocumentoSustento
                {
                    CodDocumento = EnumTipoDocumento.Factura,
                    NumDocumento = "001-001-000000123",
                    FechaEmisionDocumento = DateTime.Now
                }
            }
        }
    };

    // 2️⃣ Mapear a XML
    var xml = ComprobanteRetencion_1_0_0Mapper.Map(retencion);

    // 3️⃣ Firmar con certificado
    certificadoService.CargarDesdeP12("ruta_certificado.p12", "password");
    xml = certificadoService.FirmarDocumento(xml);

    // 4️⃣ Enviar al SRI
    var envio = await webService.ValidarComprobanteAsync(xml);
    if (!envio.Ok) return BadRequest(envio);

    var autorizacion = await webService.AutorizacionComprobanteAsync(retencion.InfoTributaria.ClaveAcceso);

    // 5️⃣ Generar PDF
    rIDEService.Retencion_1_0_0(retencion, "/ruta/FACTURA.pdf");

    return Ok(autorizacion);
}
