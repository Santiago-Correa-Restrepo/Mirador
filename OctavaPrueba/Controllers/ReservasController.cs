using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OctavaPrueba.Models;
using OctavaPrueba.Models.ViewModels;

namespace OctavaPrueba.Controllers
{
    public class ReservasController : Controller
    {
        private readonly BdMiradorContext _context;

        public ReservasController(BdMiradorContext context)
        {
            _context = context;
        }
       


        // GET: Reservas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Consultar todas las reservas y sus detalles relacionados
            var reservas = await _context.Reservas
                .Include(r => r.NroDocumentoClienteNavigation) // Incluye la relación con el cliente, si es necesario
                .Include(r => r.IdEstadoReservaNavigation) // Incluye la relación con el estado de reserva
                .Include(r => r.MetodoPagoNavigation) // Incluye la relación con el método de pago
                .Include(r => r.DetalleReservaPaquetes) // Incluye la relación con los paquetes si hay una tabla relacional
                .ThenInclude(dp => dp.IdPaqueteNavigation) // Incluye el paquete en cada detalle, si corresponde
                .Select(static r => new CrearReservaVM
                {
                    IdReserva = r.IdReserva,
                    NroDocumentoCliente = r.NroDocumentoCliente != null ? $"{r.NroDocumentoClienteNavigation.Nombres} {r.NroDocumentoClienteNavigation.Apellidos}" : "Cliente no encontrado",
                    MetodoPago = r.MetodoPagoNavigation.IdMetodoPago,
                    IdEstadoReserva = r.IdEstadoReservaNavigation != null ? r.IdEstadoReservaNavigation.IdEstadoReserva : 0, // o cualquier valor predeterminado que uses para "no disponible"
                    FechaReserva = r.FechaReserva,
                    MontoTotal = r.MontoTotal,
                    // Aquí puedes agregar más campos que quieras mostrar en la tabla de `Index`
                })
                .ToListAsync();

            return View(reservas);
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.DetalleReservaPaquetes) // Incluye la colección de paquetes
                .Include(r => r.DetalleReservaServicios) // Incluye la colección de servicios
                .Include(r => r.IdEstadoReservaNavigation)
                .Include(r => r.MetodoPagoNavigation)
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }
        // Método para obtener la lista de paquetes disponibles
        private List<PaqueteInfoVM> ObtenerPaquetes()
        {
            return _context.Paquetes
                .Select(p => new PaqueteInfoVM
                {
                    IdPaquete = p.IdPaquete,
                    Nombre = p.NomPaquete,
                    Precio = p.Precio,
                    Descripcion = p.Descripcion
                })
                .ToList();
        }

        // Método para obtener la lista de servicios disponibles
        private List<ServiciosVM> ObtenerServicios()
        {
            return _context.Servicios
                .Select(s => new ServiciosVM
                {
                    IdServicio = s.IdServicio,
                    Nombre = s.NomServicio,
                    Precio = s.Precio,
                    
                })
                .ToList();
        }

        // Método para obtener la lista de métodos de pago disponibles
        private List<MetodoPagoVM> ObtenerMetodosPago()
        {
            return _context.MetodoPagos
                .Select(mp => new MetodoPagoVM
                {
                    IdMetodoPago = mp.IdMetodoPago,
                    NombrePago = mp.NomMetodoPago
                })
                .ToList();
        }

        // Método para obtener la lista de estados de reserva disponibles
        private List<EstadoReservaVM> ObtenerEstadosReserva()
        {
            return _context.EstadosReservas
                .Select(er => new EstadoReservaVM
                {
                    IdEstado = er.IdEstadoReserva,
                    NombreEstado = er.NombreEstadoReserva
                })
                .ToList();
        }
        // GET: Reservas/Create

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CrearReservaVM
            {
                PaquetesDisponibles = ObtenerPaquetes(),
                ServiciosDisponibles = ObtenerServicios(),
                MetodosPago = ObtenerMetodosPago(),
                EstadosReserva = ObtenerEstadosReserva()
            };

            return View(model);
        }
        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{


        //    try
        //    {
        //        // Consultar los métodos de pago
        //        var metodosPago = await _context.MetodoPagos
        //            .Select(mp => new MetodoPagoVM
        //            {
        //                IdMetodoPago = mp.IdMetodoPago,
        //                NombrePago = mp.NomMetodoPago
        //            })
        //            .ToListAsync() ?? new List<MetodoPagoVM>();

        //        // Consultar los estados de reserva
        //        var estadosReserva = await _context.EstadosReservas
        //            .Select(er => new EstadoReservaVM
        //            {
        //                IdEstado = er.IdEstadoReserva,
        //                NombreEstado = er.NombreEstadoReserva
        //            })
        //            .ToListAsync() ?? new List<EstadoReservaVM>();

        //        // Consultar los paquetes
        //        var paquetes = await _context.Paquetes
        //            .Select(p => new PaqueteInfoVM
        //            {
        //                IdPaquete = p.IdPaquete,
        //                Nombre = p.NomPaquete,
        //                Precio = p.Precio,
        //                Descripcion = p.Descripcion,
        //                Habitacion = p.IdHabitacionNavigation.Nombre
        //            })
        //            .ToListAsync() ?? new List<PaqueteInfoVM>();

        //        // Consultar los servicios
        //        var servicios = await _context.Servicios
        //            .Select(s => new ServiciosVM
        //            {
        //                IdServicio = s.IdServicio,
        //                Nombre = s.NomServicio,
        //                Precio = s.Precio,
        //            })
        //            .ToListAsync() ?? new List<ServiciosVM>();

        //        // Crear el ViewModel y establecer valores predeterminados
        //        var viewModel = new CrearReservaVM
        //        {
        //            ServiciosDisponibles = servicios,
        //            PaquetesDisponibles = paquetes,
        //            Subtotal = 0,
        //            MontoTotal = 0,
        //            IVA = 0.19m, // IVA predefinido al 19%
        //            Descuento = 0,
        //            MetodosPago = metodosPago,
        //            EstadosReserva = estadosReserva,
        //            FechaReserva = DateTime.Now, // Obtiene la fecha y hora actual
        //            FechaInicio = DateTime.Today,           // O cualquier otra fecha por defecto válida
        //            FechaFinalizacion = DateTime.Today

        //        };

        //        return View(viewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error al cargar paquetes: {ex.Message}");
        //        return View(new CrearReservaVM
        //        {
        //            ServiciosDisponibles = new List<ServiciosVM>(),
        //            PaquetesDisponibles = new List<PaqueteInfoVM>(),
        //            MetodosPago = new List<MetodoPagoVM>(),
        //            EstadosReserva = new List<EstadoReservaVM>()
        //        });
        //    }
        //}
        //[HttpGet]
        public async Task<IActionResult> BuscarCliente(string nroDocumento)
        {
            try
            {
                var cliente = await _context.Clientes
                    .Include(c => c.IdTipoDocumentoNavigation)
                    .FirstOrDefaultAsync(c => c.NroDocumento == nroDocumento);

                if (cliente != null)
                {
                    
                    var clienteInfo = new ClienteInfoVM
                    {
                        NroDocumento = cliente.NroDocumento,
                        Nombre = cliente.Nombres,
                        Apellido = cliente.Apellidos,
                        Correo = cliente.Correo,
                        Celular = cliente.Celular,
                        TipoDocumento = cliente.IdTipoDocumentoNavigation.NomTipoDcumento // Ajusta si el nombre es diferente
                    };
                    return Json(clienteInfo);
                }
                return Json(new { error = "Cliente no encontrado" });
            }
            catch (Exception ex)
            {
                // Captura cualquier error y devuélvelo para ver qué ocurre
                return Json(new { error = "Error al procesar la solicitud: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CrearReservaVM model)
        {
            if (!ModelState.IsValid)
            {
                model.PaquetesDisponibles = ObtenerPaquetes();
                model.ServiciosDisponibles = ObtenerServicios();
                model.MetodosPago = ObtenerMetodosPago();
                model.EstadosReserva = ObtenerEstadosReserva();
                return View(model);
            }

            var nuevaReserva = new Reserva
            {
                NroDocumentoCliente = model.NroDocumentoCliente,
                MetodoPago = model.MetodoPago,
                IdEstadoReserva = model.IdEstadoReserva,
                FechaInicio = model.FechaInicio,
                FechaFinalizacion = model.FechaFinalizacion,
                FechaReserva = model.FechaReserva,
                MontoTotal = model.MontoTotal,
                SubTotal = (double)model.Subtotal,
                Iva = model.Iva,
                Descuento = (double)model.Descuento,
            };
            _context.Reservas.Add(nuevaReserva);
            await _context.SaveChangesAsync(); // Guardar para obtener el IdReserva generado

            // **Guardar Paquetes Seleccionados en DetalleReservaPaquete**
            foreach (var paquete in model.PaquetesSeleccionados)
            {
                var detallePaquete = new DetalleReservaPaquete
                {
                    IdReserva = nuevaReserva.IdReserva, // Usa el Id generado
                    IdPaquete = paquete.IdPaquete       // Usa el Id del paquete seleccionado
                };
                _context.DetalleReservaPaquetes.Add(detallePaquete);
            }

            // **Guardar Servicios Seleccionados en DetalleReservaServicio**
            foreach (var servicio in model.ServiciosSeleccionados)
            {
                var detalleServicio = new DetalleReservaServicio
                {
                    IdReserva = nuevaReserva.IdReserva, // Usa el Id generado
                    IdServicio = servicio.IdServicio    // Usa el Id del servicio seleccionado
                };
                _context.DetalleReservaServicios.Add(detalleServicio);
            }

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //[httppost]
        //public async task<iactionresult> create(crearreservavm model)
        //{

        //    if (!modelstate.isvalid)
        //    {




        //        model.serviciosdisponibles = await _context.servicios.select(s => new serviciosvm
        //        {
        //            idservicio = s.idservicio,
        //            nombre = s.nomservicio,
        //            precio = s.precio
        //        }).tolistasync();

        //        model.paquetesdisponibles = await _context.paquetes.select(p => new paqueteinfovm
        //        {
        //            idpaquete = p.idpaquete,
        //            nombre = p.nompaquete,
        //            precio = p.precio
        //        }).tolistasync();



        //        model.metodospago = await _context.metodopagos.select(mp => new metodopagovm
        //        {
        //            idmetodopago = mp.idmetodopago,
        //            nombrepago = mp.nommetodopago
        //        }).tolistasync();

        //        model.estadosreserva = await _context.estadosreservas.select(er => new estadoreservavm
        //        {
        //            idestado = er.idestadoreserva,
        //            nombreestado = er.nombreestadoreserva
        //        }).tolistasync();
        //    }

        //    aquí puedes añadir la lógica que se ejecutará si modelstate es válido, por ejemplo:
        //    crear y guardar la reserva

        //    using (var transaction = _context.database.begintransaction())
        //    {

        //        try
        //        {
        //            var reserva = new reserva
        //            {

        //                nrodocumentocliente = model.nrodocumentocliente,
        //                metodopago = model.metodopago,
        //                idestadoreserva = model.idestadoreserva,
        //                fechainicio = model.fechainicio,
        //                fechafinalizacion = model.fechafinalizacion,
        //                fechareserva = model.fechareserva,
        //                montototal = model.montototal,
        //                subtotal = (double)model.subtotal,
        //                iva = model.iva,
        //                descuento = (double)model.descuento,

        //            };
        //            _context.reservas.add(reserva);
        //            await _context.savechangesasync();


        //            crear las relaciones en detallereservapaquete
        //            foreach (var paquete in model.paquetesseleccionados)
        //            {
        //                var detallereservapaquete = new detallereservapaquete
        //                {
        //                    idreserva = reserva.idreserva,
        //                    idpaquete = paquete.idpaquete
        //                };
        //                _context.detallereservapaquetes.add(detallereservapaquete);
        //            }

        //            crear las relaciones en detallereservaservicio
        //            foreach (var servicio in model.serviciosseleccionados)
        //            {
        //                var detallereservaservicio = new detallereservaservicio
        //                {
        //                    idreserva = reserva.idreserva,
        //                    idservicio = servicio.idservicio
        //                };
        //                _context.detallereservaservicios.add(detallereservaservicio);
        //            }

        //            4.confirmar todos los cambios
        //           await _context.savechangesasync(); // guardar todos los cambios
        //            return redirecttoaction("index");

        //        }
        //        catch (exception ex)
        //        {
        //            transaction.rollback();
        //            manejo de errores
        //            modelstate.addmodelerror("", "ocurrió un error al guardar la reserva.");
        //            return view(model);
        //        }
        //    }
        //}
        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["IdEstadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "IdEstadoReserva", reserva.IdEstadoReserva);
            ViewData["MetodoPago"] = new SelectList(_context.MetodoPagos, "IdMetodoPago", "IdMetodoPago", reserva.MetodoPago);
            ViewData["NroDocumentoCliente"] = new SelectList(_context.Clientes, "NroDocumento", "NroDocumento", reserva.NroDocumentoCliente);
            ViewData["NroDocumentoUsuario"] = new SelectList(_context.Usuarios, "NroDocumento", "NroDocumento", reserva.NroDocumentoUsuario);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReserva,NroDocumentoCliente,NroDocumentoUsuario,FechaReserva,FechaInicio,FechaFinalizacion,SubTotal,Descuento,Iva,MontoTotal,MetodoPago,NroPersonas,IdEstadoReserva")] Reserva reserva)
        {
            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.IdReserva))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstadoReserva"] = new SelectList(_context.EstadosReservas, "IdEstadoReserva", "IdEstadoReserva", reserva.IdEstadoReserva);
            ViewData["MetodoPago"] = new SelectList(_context.MetodoPagos, "IdMetodoPago", "IdMetodoPago", reserva.MetodoPago);
            ViewData["NroDocumentoCliente"] = new SelectList(_context.Clientes, "NroDocumento", "NroDocumento", reserva.NroDocumentoCliente);
            ViewData["NroDocumentoUsuario"] = new SelectList(_context.Usuarios, "NroDocumento", "NroDocumento", reserva.NroDocumentoUsuario);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.IdEstadoReservaNavigation)
                .Include(r => r.MetodoPagoNavigation)
                .Include(r => r.NroDocumentoClienteNavigation)
                .Include(r => r.NroDocumentoUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.IdReserva == id);
        }
    }
}
