const api_url =
	"https://localhost:44304/api/workplace";

async function getWorkplaces() {
	const response = await fetch(api_url);
	
	var data = await response.json();
	if (response) {
		hideloader();
	}
	showList(data);
}

getWorkplaces();

async function getWorkplace(id) {
	const response = await fetch(`${api_url}/${id}`);
	
	var data = await response.json();
	showDetails(data);
	return data;
}

async function deleteWorkplace(id) {
	fetch(`${api_url}/${id}`, {
	  method: 'DELETE'
	})
	.then(() => {
		getWorkplaces();
	})
	.catch(error => console.error('Unable to delete item.', error));
}

async function addWorkplace() {
	let nameTextbox = document.getElementById('name');
	let streetTextbox = document.getElementById('street');
	let buildingNumberTextbox = document.getElementById('buildingNumber');
	let apartmentNumberTextbox = document.getElementById('apartmentNumber');
	let postalCodeTextbox = document.getElementById('postalCode');
	let cityTextbox = document.getElementById('city');
	let provinceTextbox = document.getElementById('province');
  
	const workplace = {
        name: nameTextbox.value.trim(),
        street: streetTextbox.value.trim(),
        buildingNumber: buildingNumberTextbox.value.trim(),
        apartmentNumber: apartmentNumberTextbox.value.trim(),
        postalCode: postalCodeTextbox.value.trim(),
        city: cityTextbox.value.trim(),
        province: provinceTextbox.value.trim()
	};

	fetch(api_url, {
		method: 'POST',
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(workplace)
	})
	.then(response => response.json())
	.then(data => {
		clearForm();
		showAndHideElement("form");
		showAndHideElement("added");
		document.querySelector("#added").innerHTML = "Dodano"; 
		getWorkplaces();
		getWorkplace(data.Id);
	})
	.catch(error => console.error('Unable to add item.', error));
}

async function updateWorkplace() {
	let idTextbox = document.getElementById('id');
	let addressIdTextbox = document.getElementById('addressId');
	let nameTextbox = document.getElementById('name');
	let streetTextbox = document.getElementById('street');
	let buildingNumberTextbox = document.getElementById('buildingNumber');
	let apartmentNumberTextbox = document.getElementById('apartmentNumber');
	let postalCodeTextbox = document.getElementById('postalCode');
	let cityTextbox = document.getElementById('city');
	let provinceTextbox = document.getElementById('province');
  
	const workplace = {
        id: idTextbox.value,
        addressId: addressIdTextbox.value,
        name: nameTextbox.value.trim(),
        street: streetTextbox.value.trim(),
        buildingNumber: buildingNumberTextbox.value.trim(),
        apartmentNumber: apartmentNumberTextbox.value.trim(),
        postalCode: postalCodeTextbox.value.trim(),
        city: cityTextbox.value.trim(),
        province: provinceTextbox.value.trim()
	};
	fetch(`${api_url}/${workplace.id}`, {
		method: 'PUT',
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(workplace)
	})
	.then(() => {
		clearForm();
		showAndHideElement("form");
		showAndHideElement("added");
		document.querySelector("#added").innerHTML = "Zaktualizowano"; 
		getWorkplaces();
		getWorkplace(workplace.id);
	})
	.catch(error => console.error('Unable to add item.', error));
}

//------------     Functions      ------------

function submitForm() {
	const idTextbox = document.getElementById('id');
	if(idTextbox.value == 0) {
		addWorkplace();
	}
	else {
		updateWorkplace();
	}
}

function showUpdateForm(id) {
	clearForm();
	document.querySelector("#form").style.display = "block";

	getWorkplace(id).then(workplace => {
        document.getElementById('id').value = workplace.Id;
        document.getElementById('addressId').value = workplace.AddressId;
        document.getElementById('name').value = workplace.Name;
        document.getElementById('street').value = workplace.Address.Street;
        document.getElementById('buildingNumber').value = workplace.Address.BuildingNumber;
        document.getElementById('apartmentNumber').value = workplace.Address.ApartmentNumber;
        document.getElementById('postalCode').value = workplace.Address.PostalCode;
        document.getElementById('city').value = workplace.Address.City;
        document.getElementById('province').value = workplace.Address.Province;
	});
}


function clearForm() {
	document.getElementById('id').value = 0;
	document.getElementById('addressId').value = 0;
	document.getElementById('name').value = "";
	document.getElementById('street').value = "";
	document.getElementById('buildingNumber').value = "";
	document.getElementById('apartmentNumber').value = "";
	document.getElementById('postalCode').value = "";
	document.getElementById('city').value = "";
	document.getElementById('province').value = "";
}

function showDetails(workplace) {
	let doctors = "";
    
	if(workplace.Doctors.length > 0){
		workplace.Doctors.forEach(doctor => {
			doctors += `<li>${doctor.FirstName} ${doctor.Surname} - ${doctor.Specialization.Name}</li>`;
		});
	} else {
		doctors = "brak"
	}

	let workplaceDetails = `<div class="p-5">
		<div class="row">
			<div class="col-lg-12"><h4>${workplace.Name}</h4></div>
		</div>
		<div class="row">
			<div class="col-md-12">ul. ${workplace.Address.Street} ${workplace.Address.BuildingNumber}/${workplace.Address.ApartmentNumber}</div>
		</div>
		<div class="row">
			<div class="col-md-12">${workplace.Address.PostalCode} ${workplace.Address.City}, ${workplace.Address.Province}</div>
		</div>
		<div class="row">
			<div class="col-12 my-2">
				<b>Pracujący lekarze:</b>
				<ul>${doctors}</ul>
			</div>
		</div>
		<div class="row">
			<div class="col-lg-1 mt-3"><span class="btn btn-danger" onclick="deleteWorkplace(${workplace.Id})">Usuń</span></div>
			<div class="col-lg-1 mt-3"><span class="btn btn-warning" onclick="showUpdateForm(${workplace.Id})">Edytuj</span></div>
		</div>
	</div>`;
	
	document.querySelector("#workplaceDetails"+workplace.Id).innerHTML = workplaceDetails;
	showAndHideElement("workplaceDetails" + workplace.Id);
}

function showList(data) {
	let list ="";
	
	for (let workplace of data) {
		list += `<li onclick="getWorkplace(${workplace.Id})" class="workplaceDetails list-group-item list-group-item-action border border-secondary rounded">
					<span>${workplace.Name}</span>
				</li>
				<li style="display:none;" id="workplaceDetails${workplace.Id}" class="list-group-item border border-light rounded"></li>`;
	}

	document.querySelector("#workplaces").innerHTML = list;
}

function hideloader() {
	document.getElementById('loading').style.display = 'none';
}

function showAndHideElement(name) {
	var x = document.getElementById(name);

	if (x.style.display === "block") {
		x.style.display = "none";
	} else {
		x.style.display = "block";
	}
}