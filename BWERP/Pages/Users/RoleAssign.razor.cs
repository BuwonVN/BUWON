using Blazored.Toast.Services;
using BWERP.Models.Role;
using BWERP.Models.Task;
using BWERP.Repositories.Interfaces;
using Microsoft.AspNetCore.Components;
using System;

namespace BWERP.Pages.Users
{
    public partial class RoleAssign
    {
        [Inject] private IUserApiClient _userApiClient { get; set; }
        [Inject] private IToastService _toastService { get; set; }
        [Inject] private IRoleApiClient _roleApiClient { get;set; }

        protected bool ShowDialog { get; set; }

        private RoleAssignRequest Model { set; get; } = new RoleAssignRequest();
        private List<RoleViewDto> _roleViewDtos;
        private Guid _userid;

        [Parameter]
        public EventCallback<bool> CloseEventCallbak { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _roleViewDtos = await _roleApiClient.GetListRole();
        }

        protected async override Task OnParametersSetAsync()
        {
            if (_userid != Guid.Empty)
            {
                var user = await _userApiClient.GetUserById(_userid.ToString());
                Model.RoleId = user.RoleId;
            }
        }

        public void Show(Guid userid)
        {
            ShowDialog = true;
            _userid = userid;
            StateHasChanged();
        }

        private void Hide()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async Task HandleAssignSubmit()
        {
            var result = await _roleApiClient.AssignRole(_userid, Model);
            if (result)
            {
                ShowDialog = false;
                await CloseEventCallbak.InvokeAsync(true);
            }
            else
            {
                _toastService.ShowError("Assign role failed");
            }
        }
    }
}
