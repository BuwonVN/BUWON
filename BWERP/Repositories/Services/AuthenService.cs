using Blazored.LocalStorage;
using BWERP.Models.User;
using BWERP.Repositories.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BWERP.Repositories.Services
{
	public class AuthenService : IAuthenService
	{
		public HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;
		private readonly AuthenticationStateProvider _authenticationStateProvider;

		public AuthenService(HttpClient httpClient,
			ILocalStorageService localStorage,
			AuthenticationStateProvider authenticationStateProvider)
		{
			_httpClient = httpClient;
			_localStorage = localStorage;
			_authenticationStateProvider = authenticationStateProvider;
		}
		public async Task<LoginResponse> Login(LoginRequest loginRequest)
		{
			var result = await _httpClient.PostAsJsonAsync("/api/login", loginRequest);
			var content = await result.Content.ReadAsStringAsync();
			var loginResponse = JsonSerializer.Deserialize<LoginResponse>(content,
				new JsonSerializerOptions()
				{
					PropertyNameCaseInsensitive = true
				});
			if (!result.IsSuccessStatusCode)
			{
				return loginResponse;
			}
			await _localStorage.SetItemAsync("authToken", loginResponse.Token);
			((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginRequest.UserName);
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
			return loginResponse;
		}

		public async Task Logout()
		{
			await _localStorage.RemoveItemAsync("authToken");
			((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
			_httpClient.DefaultRequestHeaders.Authorization = null;
		}
	}
}
