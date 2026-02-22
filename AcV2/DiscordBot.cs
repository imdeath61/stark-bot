using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace AcV2
{
    public class DiscordBot
    {
        private DiscordSocketClient _client = null!;
        private string _token;
        private readonly string apiUrl = "https://localhost:7109"; // ¡CAMBIA ESTO A LA URL DE TU API!
        private readonly ulong canalLogsId = 0; // Opcional: Pon el ID de un canal de Discord para los logs

        public DiscordBot(string token)
        {
            _token = token;
        }

        public async Task IniciarAsync()
        {
            // Mensajes de depuración para ver en ventana Salida
            System.Diagnostics.Debug.WriteLine("🚀 [BOT] Intentando iniciar bot de Discord...");

            try
            {
                System.Diagnostics.Debug.WriteLine("📦 [BOT] Creando configuración...");
                var config = new DiscordSocketConfig
                {
                    GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
                };

                System.Diagnostics.Debug.WriteLine("🤖 [BOT] Creando cliente...");
                _client = new DiscordSocketClient(config);

                _client.Log += (msg) =>
                {
                    System.Diagnostics.Debug.WriteLine($"[BOT LOG] {msg}");
                    Console.WriteLine(msg.ToString());
                    return Task.CompletedTask;
                };

                _client.MessageReceived += ManejarMensajeAsync;
                _client.Ready += () =>
                {
                    System.Diagnostics.Debug.WriteLine("✅ [BOT] ¡CONECTADO A DISCORD! Listo para recibir comandos.");
                    Console.WriteLine("✅ Bot conectado a Discord!");
                    return Task.CompletedTask;
                };

                System.Diagnostics.Debug.WriteLine("🔑 [BOT] Iniciando sesión con token...");
                await _client.LoginAsync(TokenType.Bot, _token);

                System.Diagnostics.Debug.WriteLine("🚀 [BOT] Iniciando cliente...");
                await _client.StartAsync();

                System.Diagnostics.Debug.WriteLine("✅ [BOT] Método IniciarAsync completado");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ [BOT] ERROR CRÍTICO: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"📋 [BOT] Detalle: {ex}");
                Console.WriteLine($"❌ Error al iniciar bot: {ex.Message}");
            }
        }

        private Task LogAsync(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task ManejarMensajeAsync(SocketMessage message)
        {
            if (message.Author.IsBot) return;

            // Solo usuarios con permisos de administrador en el servidor pueden usar estos comandos
            if (message.Author is SocketGuildUser user && !user.GuildPermissions.Administrator)
            {
                return; // No es admin, no puede usar comandos
            }

            // Comandos
            if (message.Content.StartsWith("!usuarios"))
            {
                await ListarUsuarios(message.Channel);
            }

            if (message.Content.StartsWith("!sesiones"))
            {
                await VerSesiones(message.Channel);
            }

            if (message.Content.StartsWith("!crear"))
            {
                // !crear usuario contraseña
                var parts = message.Content.Split(' ');
                if (parts.Length == 3)
                {
                    await CrearUsuario(parts[1], parts[2], message.Channel);
                }
                else
                {
                    await message.Channel.SendMessageAsync("❌ Uso correcto: `!crear <usuario> <contraseña>`");
                }
            }

            if (message.Content.StartsWith("!eliminar"))
            {
                // !eliminar usuario
                var parts = message.Content.Split(' ');
                if (parts.Length == 2)
                {
                    await EliminarUsuario(parts[1], message.Channel);
                }
                else
                {
                    await message.Channel.SendMessageAsync("❌ Uso correcto: `!eliminar <usuario>`");
                }
            }
        }

        // --- Métodos que se comunican con la API ---

        private async Task ListarUsuarios(ISocketMessageChannel canal)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(5);

                var response = await client.GetAsync("/api/usuarios");
                if (response.IsSuccessStatusCode)
                {
                    var usuarios = await response.Content.ReadFromJsonAsync<List<Usuario>>();
                    if (usuarios != null && usuarios.Any())
                    {
                        var mensaje = "**📋 Lista de Usuarios Registrados:**\n";
                        foreach (var u in usuarios)
                        {
                            mensaje += $"- **{u.Username}** (Activo: {u.Activo}) - Registrado: {u.FechaRegistro:dd/MM/yyyy}\n";
                        }
                        await canal.SendMessageAsync(mensaje);
                    }
                    else
                    {
                        await canal.SendMessageAsync("📭 No hay usuarios registrados.");
                    }
                }
                else
                {
                    await canal.SendMessageAsync("❌ Error al obtener usuarios de la API.");
                }
            }
            catch (Exception ex)
            {
                await canal.SendMessageAsync($"❌ Error de conexión con la API: {ex.Message}");
            }
        }

        private async Task VerSesiones(ISocketMessageChannel canal)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(5);

                var response = await client.GetAsync("/api/sesiones");
                if (response.IsSuccessStatusCode)
                {
                    var sesiones = await response.Content.ReadFromJsonAsync<List<Sesion>>();
                    if (sesiones != null && sesiones.Any())
                    {
                        var mensaje = "**📊 Últimas Sesiones:**\n";
                        foreach (var s in sesiones)
                        {
                            mensaje += $"- **{s.Username}** desde IP: `{s.IP}` - HWID: `{s.HWID}` - {s.Fecha:HH:mm dd/MM}\n";
                        }
                        await canal.SendMessageAsync(mensaje);
                    }
                    else
                    {
                        await canal.SendMessageAsync("📭 No hay sesiones registradas.");
                    }
                }
                else
                {
                    await canal.SendMessageAsync("❌ Error al obtener sesiones de la API.");
                }
            }
            catch (Exception ex)
            {
                await canal.SendMessageAsync($"❌ Error de conexión con la API: {ex.Message}");
            }
        }

        private async Task CrearUsuario(string username, string password, ISocketMessageChannel canal)
        {
            try
            {
                var nuevoUsuario = new { Username = username, Password = password };

                using var client = new HttpClient();
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(5);

                var response = await client.PostAsJsonAsync("/api/register", nuevoUsuario);
                if (response.IsSuccessStatusCode)
                {
                    await canal.SendMessageAsync($"✅ Usuario **{username}** creado correctamente.");
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    await canal.SendMessageAsync($"❌ Error al crear usuario: {error}");
                }
            }
            catch (Exception ex)
            {
                await canal.SendMessageAsync($"❌ Error de conexión con la API: {ex.Message}");
            }
        }

        private async Task EliminarUsuario(string username, ISocketMessageChannel canal)
        {
            // Este endpoint no lo hemos creado en la API, pero es un ejemplo de cómo sería.
            await canal.SendMessageAsync("⚠️ La función de eliminar usuario no está implementada en la API aún.");
            // Aquí iría la llamada a un endpoint como: client.DeleteAsync($"/api/usuarios/{username}");
        }

        // Clases para deserializar (deben coincidir con las de la API)
        private class Usuario
        {
            public string Username { get; set; } = "";
            public DateTime FechaRegistro { get; set; }
            public bool Activo { get; set; }
        }

        private class Sesion
        {
            public string Username { get; set; } = "";
            public string IP { get; set; } = "";
            public string HWID { get; set; } = "";
            public DateTime Fecha { get; set; }
        }
    }
}