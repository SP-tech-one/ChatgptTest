(function () {
    const form = document.getElementById('registration-form');
    if (!form) {
        return;
    }

    const clearBtn = document.getElementById('clear-storage');
    const recordsTableBody = document.querySelector('#records-table tbody');
    const noRecordsMessage = document.getElementById('no-records');
    const recordCountBadge = document.getElementById('record-count');
    const errorsContainer = document.getElementById('form-errors');
    const definitionElement = document.getElementById('registration-form-definition');
    const storageKey = 'registration-records';
    const endpoint = form.dataset.endpoint;

    const formDefinition = definitionElement?.textContent ? JSON.parse(definitionElement.textContent) : null;
    const fieldErrorPrefixes = {
        'Name.FirstName': 'firstName',
        'Name.LastName': 'lastName',
        'Email': 'email',
        'PhoneNumber': 'phone',
        'BirthDate': 'birthDate',
        'Gender': 'gender',
        'Address.City': 'city',
        'Address.Full': 'address'
    };

    const readRecords = () => {
        try {
            const raw = window.localStorage.getItem(storageKey);
            return raw ? JSON.parse(raw) : [];
        } catch (error) {
            console.error('خطا در خواندن اطلاعات از localStorage', error);
            return [];
        }
    };

    const writeRecords = (records) => {
        try {
            window.localStorage.setItem(storageKey, JSON.stringify(records));
        } catch (error) {
            console.error('خطا در ذخیره‌سازی در localStorage', error);
        }
    };

    const formatDate = (isoString) => {
        if (!isoString) {
            return '-';
        }

        const date = new Date(isoString);
        return new Intl.DateTimeFormat('fa-IR', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        }).format(date);
    };

    const renderRecords = () => {
        const records = readRecords();
        recordsTableBody.innerHTML = '';

        if (!records.length) {
            noRecordsMessage?.classList.remove('d-none');
            if (recordCountBadge) {
                recordCountBadge.textContent = '0';
            }
            return;
        }

        noRecordsMessage?.classList.add('d-none');
        if (recordCountBadge) {
            recordCountBadge.textContent = records.length.toString();
        }

        records
            .sort((a, b) => new Date((b.createdAt ?? b.CreatedAt) || 0).getTime() - new Date((a.createdAt ?? a.CreatedAt) || 0).getTime())
            .forEach((record, index) => {
                const row = document.createElement('tr');
                const fullName = record.fullName || [record.firstName, record.lastName].filter(Boolean).join(' ').trim() || '-';
                const genderLine = record.genderDisplayName ? `<div class="text-muted small">${record.genderDisplayName}</div>` : '';
                const email = record.email || '-';
                const createdAtValue = record.createdAt ?? record.CreatedAt;

                row.innerHTML = `
                    <th scope="row">${index + 1}</th>
                    <td>
                        <div class="fw-semibold">${fullName}</div>
                        ${genderLine}
                    </td>
                    <td>${email}</td>
                    <td>${record.phoneNumber || '-'}</td>
                    <td>${record.city || '-'}</td>
                    <td>${formatDate(createdAtValue)}</td>`;

                recordsTableBody.appendChild(row);
            });
    };

    const formControls = () => Array.from(form.querySelectorAll('input, select, textarea'));

    const markInvalidFields = () => {
        formControls().forEach(control => {
            if (!control.checkValidity()) {
                control.classList.add('is-invalid');
            } else {
                control.classList.remove('is-invalid');
            }
        });
    };

    const clearFieldValidationStyles = () => {
        formControls().forEach(control => control.classList.remove('is-invalid'));
    };

    const clearServerErrors = () => {
        if (errorsContainer) {
            errorsContainer.classList.add('d-none');
            errorsContainer.innerHTML = '';
        }
    };

    const showServerErrors = (errors) => {
        if (!errorsContainer) {
            return;
        }

        const messages = Array.isArray(errors) ? errors : [];
        if (!messages.length) {
            return;
        }

        errorsContainer.classList.remove('d-none');
        const list = document.createElement('ul');
        list.classList.add('mb-0');

        messages.forEach(error => {
            const listItem = document.createElement('li');
            listItem.textContent = error.message || 'خطایی رخ داده است.';
            list.appendChild(listItem);

            if (error.code) {
                const prefix = Object.keys(fieldErrorPrefixes).find(key => error.code.startsWith(key));
                if (prefix) {
                    const fieldId = fieldErrorPrefixes[prefix];
                    const field = document.getElementById(fieldId);
                    field?.classList.add('is-invalid');
                }
            }
        });

        errorsContainer.appendChild(list);
    };

    form.addEventListener('submit', async (event) => {
        event.preventDefault();
        form.classList.add('was-validated');
        clearServerErrors();
        markInvalidFields();

        if (!form.checkValidity()) {
            return;
        }

        const formData = new FormData(form);
        const payload = Object.fromEntries(formData.entries());
        const requestBody = {
            firstName: (payload.firstName || '').toString().trim(),
            lastName: (payload.lastName || '').toString().trim(),
            email: (payload.email || '').toString().trim(),
            phoneNumber: (payload.phone || '').toString().trim(),
            birthDate: (payload.birthDate || '').toString().trim(),
            gender: (payload.gender || '').toString().trim(),
            city: (payload.city || '').toString().trim(),
            address: (payload.address || '').toString().trim()
        };

        try {
            if (!endpoint) {
                throw new Error('آدرس سرویس اعتبارسنجی یافت نشد.');
            }

            const response = await fetch(endpoint, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(requestBody)
            });

            if (!response.ok) {
                let errors;
                try {
                    errors = await response.json();
                } catch {
                    errors = [{ message: 'خطای ناشناخته‌ای رخ داده است.' }];
                }
                showServerErrors(errors);
                return;
            }

            const record = await response.json();
            const records = readRecords();
            records.push(record);
            writeRecords(records);

            form.reset();
            form.classList.remove('was-validated');
            clearFieldValidationStyles();
            clearServerErrors();
            renderRecords();
        } catch (error) {
            console.error('خطا در ارتباط با سرویس اعتبارسنجی', error);
            showServerErrors([{ message: 'امکان برقراری ارتباط با سرویس اعتبارسنجی وجود ندارد. لطفاً دوباره تلاش کنید.' }]);
        }
    });

    form.addEventListener('input', (event) => {
        const target = event.target;
        if (target instanceof HTMLInputElement || target instanceof HTMLSelectElement || target instanceof HTMLTextAreaElement) {
            if (target.checkValidity()) {
                target.classList.remove('is-invalid');
            }
        }
    });

    clearBtn?.addEventListener('click', () => {
        if (confirm('آیا از حذف همه اطلاعات مطمئن هستید؟')) {
            writeRecords([]);
            clearFieldValidationStyles();
            clearServerErrors();
            form.classList.remove('was-validated');
            renderRecords();
        }
    });

    renderRecords();

    if (formDefinition) {
        form.dataset.definition = JSON.stringify({
            title: formDefinition.title,
            fieldCount: formDefinition.sections?.flatMap(section => section.fields || []).length || 0
        });
    }
})();
