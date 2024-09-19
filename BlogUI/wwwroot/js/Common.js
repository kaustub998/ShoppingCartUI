window.ShowToastr = (type, message) => {
    toastr.options.toastClass = "toastr";

    if (type == "success") {
        toastr.success(message, "Operation Successful", { timeOut: 5000 });
    }
    if (type == "error") {
        toastr.error(message, "Operation Failed", { timeOut: 5000 });
    }
    if (type == "warning") {
        toastr.warning(message, "Wrong Operation", { timeOut: 5000 });
    }
}

window.showDropdown = (elementId) => {
    var dropdown = document.getElementById(elementId);
    dropdown.classList.add("show");
};

window.hideDropdown = (elementId) => {
    var dropdown = document.getElementById(elementId);
    dropdown.classList.remove("show");
};