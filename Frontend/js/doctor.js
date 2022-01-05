const api_url =
	"https://localhost:44304/api/doctor";

async function getDoctors() {
	let specializationFilter = document.getElementById('specializationFilter').value;
	let response;
	if(specializationFilter == null || specializationFilter == 0) {
		response = await fetch(api_url);
	} else {
		response = await fetch(`${api_url}?specialization=${specializationFilter}`);
	}
	
	var data = await response.json();
	if (response) {
		hideloader();
	}
	console.log(data)
	showList(data);
	getSpecializationsFilter();
}




async function getDoctor(id) {
	const response = await fetch(`${api_url}/${id}`);
	
	var data = await response.json();
	showDetails(data);
	return data;
}

async function deleteDoctor(id) {
	fetch(`${api_url}/${id}`, {
	  method: 'DELETE'
	})
	.then(() => {
		getDoctors();
	})
	.catch(error => console.error('Unable to delete item.', error));
}

async function addDoctor() {
	let firstNameTextbox = document.getElementById('firstName');
	let surnameTextbox = document.getElementById('surname');
	let phoneNumberTextbox = document.getElementById('phoneNumber');
	let emailTextbox = document.getElementById('email');
	let degreeTextbox = document.getElementById('degree');
	let specializationTextbox = document.getElementById('specialization');
  
	const doctor = {
	  firstName: firstNameTextbox.value.trim(),
	  surname: surnameTextbox.value.trim(),
	  phoneNumber: phoneNumberTextbox.value.trim(),
	  email: emailTextbox.value.trim(),
	  degree: degreeTextbox.value.trim(),
	  specializationId: specializationTextbox.value
	};

	fetch(api_url, {
		method: 'POST',
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(doctor)
	})
	.then(response => response.json())
	.then(data => {
		clearForm();
		showAndHideElement("form");
		showAndHideElement("added");
		document.querySelector("#added").innerHTML = "Dodano"; 
		getDoctors();
		getDoctor(data.Id);
	})
	.catch(error => console.error('Unable to add item.', error));
}

async function updateDoctor() {
	let idTextbox = document.getElementById('id');
	let firstNameTextbox = document.getElementById('firstName');
	let surnameTextbox = document.getElementById('surname');
	let phoneNumberTextbox = document.getElementById('phoneNumber');
	let emailTextbox = document.getElementById('email');
	let degreeTextbox = document.getElementById('degree');
	let specializationTextbox = document.getElementById('specialization');
  
	const doctor = {
		id: idTextbox.value,
		firstName: firstNameTextbox.value.trim(),
		surname: surnameTextbox.value.trim(),
		phoneNumber: phoneNumberTextbox.value.trim(),
		email: emailTextbox.value.trim(),
		degree: degreeTextbox.value.trim(),
		specializationId: specializationTextbox.value
	};
	
	fetch(`${api_url}/${doctor.id}`, {
		method: 'PUT',
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(doctor)
	})
	.then(() => {
		clearForm();
		showAndHideElement("form");
		showAndHideElement("added");
		document.querySelector("#added").innerHTML = "Zaktualizowano"; 
		getDoctors();
		getDoctor(doctor.id);
	})
	.catch(error => console.error('Unable to add item.', error));
}

async function getSpecializations() {
	const response = await fetch("https://localhost:44304/api/specialization");
	
	var data = await response.json();
	fillSelect(data);
}

async function getSpecializationsFilter() {
	const response = await fetch("https://localhost:44304/api/specialization");
	
	var data = await response.json();
	fillSelectFilter(data);
}

async function assign() {
	let doctorIdTextbox = document.getElementById('assignDoctorId');
	let workplaceChechboxes = document.getElementsByClassName('workplaceChechbox');

	let workplaces = []

	for(let i = workplaceChechboxes.length - 1; i >= 0; i-- ){
		if(workplaceChechboxes[i].checked){
			workplaces.push(parseInt(workplaceChechboxes[i].value));
		}
	}

	const doctorWorkplace = {
	  doctorId: doctorIdTextbox.value,
	  workplaceIds: workplaces
	};

	fetch("https://localhost:44304/api/DoctorWorkplace", {
		method: 'POST',
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(doctorWorkplace)
	})
	.then(response => response.json())
	.then(doctor => {
		clearAssignForm();
		showAndHideElement("assignForm");
		document.querySelector("#added").innerHTML = "Przypisano"; 
		getDoctors();
		getDoctor(doctor.Id);
	})
	.catch(error => console.error('Unable to add item.', error));
}

//------------     Functions      ------------
function fillSelectFilter(data) {
	let select = document.getElementById('specializationFilter');
	select.innerHTML = "";

	let option = document.createElement('option');
	option.value = "";
	option.innerHTML = "Wszystkie";
	select.appendChild(option);

	for (let specialization of data) {
		let option = document.createElement('option');
		option.value = specialization.Name;
		option.innerHTML = specialization.Name;
		select.appendChild(option);
	}
}

function clearAssignForm() {
	document.getElementById('assignDoctorId').value = 0;
	let assignWorkplaces = document.querySelector("#assignWorkplaces");
	while (assignWorkplaces.firstChild) {
		assignWorkplaces.removeChild(assignWorkplaces.lastChild);
	  }
}

function showAssignForm(id, firstName, surname) {
	scrollTo(0,60);
	showAndHideElement("assignForm");
	document.getElementById('assignDoctorId').value = id;
	document.getElementById('assginDoctorName').innerHTML = firstName + " " + surname;
	getWorkplaces(id);
}

async function getWorkplaces(doctorId) {
	const response = await fetch("https://localhost:44304/api/workplace");
	
	var data = await response.json();
	addCheckList(data, doctorId);
}

function addCheckList(data, doctorId) {
	let checklist ="";
	
	for (let workplace of data) {
		let isWorkingHere = false;
		workplace.Doctors.forEach(d => {
			if(d.Id == doctorId) {
				isWorkingHere = true;
			} 
		});
		if(!isWorkingHere){
			checklist += `<div class="form-check">
        <input class="workplaceChechbox form-check-input" type="checkbox" id="checkbox${workplace.Id}" value="${workplace.Id}">
        <label class="form-check-label" for="checkbox${workplace.Id}">${workplace.Name}</label>
      </div>`;
		}
	}

	document.querySelector("#assignWorkplaces").innerHTML = checklist;
}

function submitForm() {
	const idTextbox = document.getElementById('id');
	if(idTextbox.value == 0) {
		addDoctor();
	}
	else {
		updateDoctor();
	}
}

function showUpdateForm(id) {
	clearForm();
	getSpecializations();
	document.querySelector("#form").style.display = "block";

	getDoctor(id).then(doctor => {
		document.getElementById('id').value = doctor.Id;
		document.getElementById('firstName').value = doctor.FirstName;
		document.getElementById('surname').value = doctor.Surname;
		document.getElementById('phoneNumber').value = doctor.PhoneNumber;
		document.getElementById('email').value = doctor.Email;
		document.getElementById('degree').value = doctor.Degree;
		document.getElementById('specialization').value = doctor.Specialization.Id;
	});
}

function clearForm() {
	document.getElementById('id').value = 0;
	document.getElementById('firstName').value = "";
	document.getElementById('surname').value = "";
	document.getElementById('phoneNumber').value = "";
	document.getElementById('email').value = "";
	document.getElementById('degree').value = "";
}

function fillSelect(data) {
	let select = document.getElementById('specialization');
	select.innerHTML = "";

	for (let specialization of data) {
		let option = document.createElement('option');
		option.value = specialization.Id;
		option.innerHTML = specialization.Name;
		select.appendChild(option);
	}
}

function hideloader() {
	document.getElementById('loading').style.display = 'none';
}

function showList(data) {
	let list ="";
	
	for (let doctor of data) {
		list += `<li onclick="getDoctor(${doctor.Id})" class="doctorDetails list-group-item list-group-item-action border border-secondary rounded">
					<span>${doctor.FirstName} ${doctor.Surname} - ${doctor.Specialization.Name}</span>
				</li>
				<li style="display:none;" id="doctorDetails${doctor.Id}" class="list-group-item border border-light rounded"></li>`;
	}

	document.querySelector("#doctors").innerHTML = list;
}

function showDetails(doctor) {
	let workplaces = "";

	if(doctor.Workplaces.length > 0){
		doctor.Workplaces.forEach(element => {
			workplaces += `<li>${element.Name}</li>`;
		});
	} else {
		workplaces = "brak"
	}

	let doctorDetails = `<div class="p-5">
		<div class="row">
			<div class="col-lg-12"><h4>${doctor.FirstName} ${doctor.Surname}</h4></div>
		</div>
		<div class="row">
			<div class="col-md-6">Specjalizacja: ${doctor.Specialization.Name}</div>
			<div class="col-md-6">Stopień naukowy: ${doctor.Degree}</div>
			<div class="col-md-6">Numer telefonu: ${doctor.PhoneNumber}</div>
			<div class="col-md-6">Email: ${doctor.Email}</div>
		</div>
		<div class="row">
			<div class="col-12 my-2">
				<b>Miejsca pracy:</b>
				<ul>${workplaces}</ul>
			</div>
		</div>
		<div class="row">
			<div class="col-lg-1 mt-3 mx-1"><span class="btn btn-danger" onclick="deleteDoctor(${doctor.Id})">Usuń</span></div>
			<div class="col-lg-1 mt-3 mx-1"><span class="btn btn-warning" onclick="showUpdateForm(${doctor.Id})">Edytuj</span></div>
			<div class="col-lg-5 mt-3 mx-1"><span class="btn btn-primary" onclick="showAssignForm(${doctor.Id}, '${doctor.FirstName}', '${doctor.Surname}')">Przypisz do miejsca pracy</span></div>
		</div>
	</div>`;
	
	document.querySelector("#doctorDetails"+doctor.Id).innerHTML = doctorDetails;
	showAndHideElement("doctorDetails" + doctor.Id);
}

function showAndHideElement(name) {
	var x = document.getElementById(name);

	if (x.style.display === "block") {
		x.style.display = "none";
	} else {
		x.style.display = "block";
	}
}
