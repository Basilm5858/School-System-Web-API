// ============================================================
// School System Frontend - matched to your current DTOs/routes
// ============================================================

const API_BASE = "http://localhost:5203/api";

const resources = {
  departments: {
    title: "Departments",
    singular: "Department",
    endpoint: "Department",
    subtitle: "Manage school departments.",
    columns: [
      { key: "id", label: "ID" },
      { key: "name", label: "Name" },
      { key: "description", label: "Description" }
    ],
    createFields: [
      { key: "name", label: "Name", type: "text", required: true },
      { key: "description", label: "Description", type: "textarea", full: true }
    ],
    updateFields: [
      { key: "name", label: "Name", type: "text", required: true },
      { key: "description", label: "Description", type: "textarea", full: true }
    ],
    createRoute: () => "Department",
    updateRoute: id => `Department/${id}`,
    deleteRoute: id => `Department?id=${id}`,
    detailsRoute: id => `Department/${id}`
  },

  classrooms: {
    title: "Classrooms",
    singular: "Classroom",
    endpoint: "ClassRooms",
    subtitle: "Manage classrooms, capacities and grade levels.",
    columns: [
      { key: "id", label: "ID" },
      { key: "name", label: "Name" },
      { key: "gradeLevel", label: "Grade Level" },
      { key: "capacity", label: "Capacity" }
    ],
    createFields: [
      { key: "name", label: "Name", type: "text", required: true },
      { key: "capacity", label: "Capacity", type: "number", required: true },
      { key: "gradeLevel", label: "Grade Level", type: "number", required: true }
    ],
    updateFields: [
      { key: "name", label: "Name", type: "text", required: true },
      { key: "capacity", label: "Capacity", type: "number", required: true },
      { key: "gradeLevel", label: "Grade Level", type: "number", required: true }
    ],
    createRoute: () => "ClassRooms",
    updateRoute: id => `ClassRooms/${id}`,
    deleteRoute: id => `ClassRooms?id=${id}`,
    detailsRoute: id => `ClassRooms/${id}`
  },

  teachers: {
    title: "Teachers",
    singular: "Teacher",
    endpoint: "Teacher",
    subtitle: "Manage teachers and department assignments.",
    columns: [
      { key: "id", label: "ID" },
      { key: "fullName", label: "Teacher" },
      { key: "email", label: "Email" },
      { key: "phoneNumber", label: "Phone" },
      { key: "salary", label: "Salary" },
      { key: "departmentName", label: "Department" }
    ],
    createFields: [
      { key: "firstName", label: "First Name", type: "text", required: true },
      { key: "lastName", label: "Last Name", type: "text", required: true },
      { key: "email", label: "Email", type: "email", required: true },
      { key: "phoneNumber", label: "Phone Number", type: "text", required: true },
      { key: "salary", label: "Salary", type: "number", required: true },
      {
        key: "departmentId", label: "Department", type: "select", required: true,
        optionsFrom: "departments", optionValue: "id", optionLabel: "name"
      }
    ],
    updateFields: [
      { key: "firstName", label: "First Name", type: "text", required: true },
      { key: "lastName", label: "Last Name", type: "text", required: true },
      { key: "email", label: "Email", type: "email", required: true },
      { key: "phoneNumber", label: "Phone Number", type: "text", required: true },
      { key: "salary", label: "Salary", type: "number", required: true },
      {
        key: "departmentId", label: "Department", type: "select", required: true,
        optionsFrom: "departments", optionValue: "id", optionLabel: "name"
      }
    ],
    createRoute: () => "Teacher",
    updateRoute: id => `Teacher?id=${id}`,
    deleteRoute: id => `Teacher?id=${id}`,
    detailsRoute: id => `Teacher/${id}`
  },

  students: {
    title: "Students",
    singular: "Student",
    endpoint: "Student",
    subtitle: "Manage student records and classroom assignments.",
    columns: [
      { key: "id", label: "ID" },
      { key: "fullName", label: "Student" },
      { key: "email", label: "Email" },
      { key: "phoneNumber", label: "Phone" },
      { key: "classRoomName", label: "Classroom" }
    ],
    createFields: [
      { key: "firstName", label: "First Name", type: "text", required: true },
      { key: "lastName", label: "Last Name", type: "text", required: true },
      { key: "phoneNumber", label: "Phone Number", type: "text", required: true },
      { key: "email", label: "Email", type: "email", required: true },
      {
        key: "classRoomId", label: "Classroom", type: "select", required: true,
        optionsFrom: "classrooms", optionValue: "id", optionLabel: "name"
      }
    ],
    updateFields: [
      { key: "firstName", label: "First Name", type: "text", required: true },
      { key: "lastName", label: "Last Name", type: "text", required: true },
      { key: "phoneNumber", label: "Phone Number", type: "text", required: true },
      { key: "email", label: "Email", type: "email", required: true },
      {
        key: "classRoomId", label: "Classroom", type: "select", required: true,
        optionsFrom: "classrooms", optionValue: "id", optionLabel: "name"
      }
    ],
    createRoute: () => "Student",
    updateRoute: id => `Student/${id}`,
    deleteRoute: id => `Student?id=${id}`,
    detailsRoute: id => `Student/${id}`,
    transformEditData: row => splitFullName(row)
  },

  subjects: {
    title: "Subjects",
    singular: "Subject",
    endpoint: "Subject",
    subtitle: "Manage subjects, grading limits and teacher assignments.",
    columns: [
      { key: "id", label: "ID" },
      { key: "name", label: "Subject" },
      { key: "maxGrade", label: "Max Grade" },
      { key: "description", label: "Description" },
      { key: "teacherName", label: "Teacher" }
    ],
    createFields: [
      { key: "name", label: "Name", type: "text", required: true },
      { key: "maxGrade", label: "Maximum Grade", type: "number", required: true },
      { key: "description", label: "Description", type: "textarea", full: true, required: true },
      {
        key: "teacherId", label: "Teacher", type: "select", required: true,
        optionsFrom: "teachers", optionValue: "id", optionLabel: "fullName"
      }
    ],
    updateFields: [
      { key: "name", label: "Name", type: "text", required: true },
      { key: "maxGrade", label: "Maximum Grade", type: "number", required: true },
      { key: "description", label: "Description", type: "textarea", full: true, required: true },
      {
        key: "teacherId", label: "Teacher", type: "select", required: true,
        optionsFrom: "teachers", optionValue: "id", optionLabel: "fullName"
      }
    ],
    createRoute: () => "Subject",
    updateRoute: id => `Subject/${id}`,
    deleteRoute: id => `Subject?id=${id}`,
    detailsRoute: id => `Subject/${id}`
  },

  enrollments: {
    title: "Enrollments",
    singular: "Enrollment",
    endpoint: "Enrollment",
    subtitle: "Manage student enrollments, subjects and grades.",
    columns: [
      { key: "id", label: "ID" },
      { key: "studentFullName", label: "Student" },
      { key: "subjectName", label: "Subject" },
      { key: "enrollmentDate", label: "Enrollment Date", format: "date" },
      { key: "grade", label: "Grade" }
    ],
    createFields: [
      {
        key: "studentId", label: "Student", type: "select", required: true,
        optionsFrom: "students", optionValue: "id", optionLabel: "fullName"
      },
      {
        key: "subjectId", label: "Subject", type: "select", required: true,
        optionsFrom: "subjects", optionValue: "id", optionLabel: "name"
      },
      { key: "grade", label: "Grade", type: "number", step: "0.01", required: true }
    ],
    updateFields: [
      {
        key: "studentId", label: "Student", type: "select", required: true,
        optionsFrom: "students", optionValue: "id", optionLabel: "fullName"
      },
      {
        key: "subjectId", label: "Subject", type: "select", required: true,
        optionsFrom: "subjects", optionValue: "id", optionLabel: "name"
      },
      { key: "grade", label: "Grade", type: "number", step: "0.01", required: true }
    ],
    createRoute: () => "Enrollment",
    updateRoute: id => `Enrollment?id=${id}`,
    deleteRoute: id => `Enrollment?id=${id}`,
    detailsRoute: id => `Enrollment/${id}`
  }
};

const state = {
  page: "dashboard",
  data: {},
  search: "",
  modal: null
};

const pageRoot = document.getElementById("pageRoot");
const pageTitle = document.getElementById("pageTitle");
const pageSubtitle = document.getElementById("pageSubtitle");
const modalBackdrop = document.getElementById("modalBackdrop");
const drawerBackdrop = document.getElementById("drawerBackdrop");
const entityForm = document.getElementById("entityForm");

document.getElementById("apiUrlText").textContent = API_BASE;
document.getElementById("dateChip").textContent =
  new Intl.DateTimeFormat("en", {
    weekday: "short",
    month: "short",
    day: "numeric",
    year: "numeric"
  }).format(new Date());

// ------------------------------------------------------------
// API
// ------------------------------------------------------------
async function api(path, options = {}) {
  const config = { ...options };
  config.headers = {
    ...(options.body ? { "Content-Type": "application/json" } : {}),
    ...(options.headers || {})
  };

  const response = await fetch(`${API_BASE}/${path}`, config);

  if (response.status === 204) {
    return { status: 204, data: null };
  }

  const text = await response.text();
  let data = null;

  if (text) {
    try { data = JSON.parse(text); }
    catch { data = text; }
  }

  if (!response.ok) {
    const error = new Error(
      typeof data === "string"
        ? data
        : data?.title || data?.message || `Request failed with status ${response.status}`
    );
    error.status = response.status;
    error.data = data;
    throw error;
  }

  return { status: response.status, data };
}

async function getList(resourceKey, force = false) {
  if (!force && Array.isArray(state.data[resourceKey])) {
    return state.data[resourceKey];
  }

  const result = await api(resources[resourceKey].endpoint);
  state.data[resourceKey] = Array.isArray(result.data) ? result.data : [];
  return state.data[resourceKey];
}

function invalidate(resourceKey) {
  delete state.data[resourceKey];
}

// ------------------------------------------------------------
// Navigation
// ------------------------------------------------------------
document.getElementById("nav").addEventListener("click", e => {
  const button = e.target.closest(".nav-item");
  if (!button) return;
  navigate(button.dataset.page);
});

document.getElementById("menuButton").addEventListener("click", () => {
  document.getElementById("sidebar").classList.toggle("open");
});

document.getElementById("refreshButton").addEventListener("click", async () => {
  if (state.page === "dashboard") {
    Object.keys(resources).forEach(invalidate);
    await renderDashboard();
  } else {
    invalidate(state.page);
    await renderResourcePage(state.page);
  }
});

function navigate(page) {
  state.page = page;
  state.search = "";

  document.querySelectorAll(".nav-item").forEach(item => {
    item.classList.toggle("active", item.dataset.page === page);
  });

  document.getElementById("sidebar").classList.remove("open");

  if (page === "dashboard") {
    pageTitle.textContent = "Dashboard";
    pageSubtitle.textContent = "School overview and quick actions";
    renderDashboard();
    return;
  }

  const config = resources[page];
  pageTitle.textContent = config.title;
  pageSubtitle.textContent = config.subtitle;
  renderResourcePage(page);
}

// ------------------------------------------------------------
// Dashboard
// ------------------------------------------------------------
async function renderDashboard() {
  pageRoot.innerHTML = `
    <section class="hero">
      <div>
        <h2>School Administration Dashboard</h2>
        <p>Manage academic structure, teachers, students, subjects and enrollments from one place.</p>
      </div>
      <div class="hero-icon">SS</div>
    </section>

    <section class="stats-grid" id="statsGrid">
      ${["Departments", "Classrooms", "Teachers", "Students"].map(label => `
        <article class="stat-card">
          <div class="stat-head">
            <span class="stat-label">${label}</span>
            <span class="stat-icon">••</span>
          </div>
          <div class="stat-value">—</div>
        </article>
      `).join("")}
    </section>

    <section class="dashboard-grid">
      <article class="panel">
        <div class="panel-head">
          <div>
            <h3>Quick Actions</h3>
            <p>Create frequently used records</p>
          </div>
        </div>
        <div class="panel-body">
          <div class="quick-actions">
            <button class="quick-action" data-create="students">
              <strong>Add Student</strong>
              <span>Create a new student record</span>
            </button>
            <button class="quick-action" data-create="teachers">
              <strong>Add Teacher</strong>
              <span>Add a teacher to a department</span>
            </button>
            <button class="quick-action" data-create="subjects">
              <strong>Add Subject</strong>
              <span>Create a subject and grading limit</span>
            </button>
            <button class="quick-action" data-create="enrollments">
              <strong>New Enrollment</strong>
              <span>Enroll a student in a subject</span>
            </button>
          </div>
        </div>
      </article>

      <article class="panel">
        <div class="panel-head">
          <div>
            <h3>Academic Summary</h3>
            <p>Current system totals</p>
          </div>
        </div>
        <div class="panel-body">
          <div class="summary-list" id="summaryList">
            <div class="loading"><span class="spinner"></span>Loading...</div>
          </div>
        </div>
      </article>
    </section>
  `;

  pageRoot.querySelectorAll("[data-create]").forEach(button => {
    button.addEventListener("click", () => openEditor(button.dataset.create, "create"));
  });

  const keys = ["departments", "classrooms", "teachers", "students", "subjects", "enrollments"];
  const results = await Promise.allSettled(keys.map(key => getList(key, true)));

  const counts = {};
  keys.forEach((key, index) => {
    counts[key] = results[index].status === "fulfilled"
      ? results[index].value.length
      : null;
  });

  const mainStats = [
    ["departments", "Departments", "D"],
    ["classrooms", "Classrooms", "C"],
    ["teachers", "Teachers", "T"],
    ["students", "Students", "S"]
  ];

  document.getElementById("statsGrid").innerHTML = mainStats.map(([key, label, icon]) => `
    <article class="stat-card">
      <div class="stat-head">
        <span class="stat-label">${label}</span>
        <span class="stat-icon">${icon}</span>
      </div>
      <div class="stat-value">${counts[key] ?? "!"}</div>
    </article>
  `).join("");

  document.getElementById("summaryList").innerHTML = `
    ${summaryRow("Subjects", counts.subjects)}
    ${summaryRow("Enrollments", counts.enrollments)}
    ${summaryRow("API", Object.values(counts).every(v => v !== null) ? "Connected" : "Partial")}
  `;

  setApiStatus(results.some(r => r.status === "fulfilled"));
}

function summaryRow(label, value) {
  return `
    <div class="summary-row">
      <div>
        <strong>${label}</strong>
        <span>Current data</span>
      </div>
      <span class="badge">${value ?? "Unavailable"}</span>
    </div>
  `;
}

// ------------------------------------------------------------
// Resource pages / tables
// ------------------------------------------------------------
async function renderResourcePage(resourceKey) {
  const config = resources[resourceKey];

  pageRoot.innerHTML = `
    <div class="toolbar">
      <div class="toolbar-left">
        <div class="search-box">
          <span>⌕</span>
          <input id="searchInput" type="search" placeholder="Search ${config.title.toLowerCase()}..." />
        </div>
        ${resourceKey === "departments" ? `
          <button class="btn ghost" id="teacherSearchButton">Search by teacher</button>
        ` : ""}
      </div>

      <div class="toolbar-right">
        <button class="btn secondary" id="reloadButton">Refresh</button>
        <button class="btn primary" id="createButton">+ Add ${config.singular}</button>
      </div>
    </div>

    <div class="table-card">
      <div id="tableHost">
        <div class="loading"><span class="spinner"></span>Loading ${config.title.toLowerCase()}...</div>
      </div>
    </div>
  `;

  document.getElementById("searchInput").addEventListener("input", e => {
    state.search = e.target.value.trim().toLowerCase();
    paintTable(resourceKey);
  });

  document.getElementById("reloadButton").addEventListener("click", async () => {
    invalidate(resourceKey);
    await loadTable(resourceKey);
  });

  document.getElementById("createButton").addEventListener("click", () => {
    openEditor(resourceKey, "create");
  });

  if (resourceKey === "departments") {
    document.getElementById("teacherSearchButton").addEventListener("click", searchDepartmentByTeacher);
  }

  await loadTable(resourceKey);
}

async function loadTable(resourceKey) {
  const host = document.getElementById("tableHost");
  if (!host) return;

  host.innerHTML = `<div class="loading"><span class="spinner"></span>Loading...</div>`;

  try {
    await getList(resourceKey, true);
    paintTable(resourceKey);
    setApiStatus(true);
  } catch (error) {
    setApiStatus(false);
    host.innerHTML = `
      <div class="empty">
        <strong>Could not load data</strong>
        <span>${escapeHtml(error.message)}</span>
      </div>
    `;
  }
}

function paintTable(resourceKey) {
  const host = document.getElementById("tableHost");
  if (!host) return;

  const config = resources[resourceKey];
  const allRows = state.data[resourceKey] || [];

  const rows = allRows.filter(row => {
    if (!state.search) return true;
    return Object.values(row || {}).some(value =>
      String(value ?? "").toLowerCase().includes(state.search)
    );
  });

  if (!rows.length) {
    host.innerHTML = `
      <div class="empty">
        <strong>No ${config.title.toLowerCase()} found</strong>
        <span>Add a ${config.singular.toLowerCase()} or change the search.</span>
      </div>
    `;
    return;
  }

  host.innerHTML = `
    <div class="table-wrap">
      <table>
        <thead>
          <tr>
            ${config.columns.map(column => `<th>${column.label}</th>`).join("")}
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          ${rows.map(row => `
            <tr>
              ${config.columns.map(column => `
                <td>${formatCell(row[column.key], column)}</td>
              `).join("")}
              <td>
                <div class="actions">
                  <button class="btn small ghost" data-view="${row.id}">View</button>
                  <button class="btn small secondary" data-edit="${row.id}">Edit</button>
                  <button class="btn small danger" data-delete="${row.id}">Delete</button>
                </div>
              </td>
            </tr>
          `).join("")}
        </tbody>
      </table>
    </div>
  `;

  host.querySelectorAll("[data-view]").forEach(button => {
    button.addEventListener("click", () => viewRecord(resourceKey, Number(button.dataset.view)));
  });

  host.querySelectorAll("[data-edit]").forEach(button => {
    button.addEventListener("click", () => openEditor(resourceKey, "edit", Number(button.dataset.edit)));
  });

  host.querySelectorAll("[data-delete]").forEach(button => {
    button.addEventListener("click", () => deleteRecord(resourceKey, Number(button.dataset.delete)));
  });
}

function formatCell(value, column) {
  if (value === null || value === undefined || value === "") {
    return `<span style="color:var(--muted)">—</span>`;
  }

  if (column.format === "date") {
    const date = new Date(value);
    return Number.isNaN(date.getTime())
      ? escapeHtml(String(value))
      : escapeHtml(date.toLocaleDateString());
  }

  return escapeHtml(String(value));
}

// ------------------------------------------------------------
// Details drawer
// ------------------------------------------------------------
async function viewRecord(resourceKey, id) {
  const config = resources[resourceKey];

  try {
    const result = await api(config.detailsRoute(id));
    const row = result.data || {};

    document.getElementById("drawerTitle").textContent = `${config.singular} Details`;
    document.getElementById("drawerSubtitle").textContent = `Record #${id}`;

    document.getElementById("drawerBody").innerHTML = `
      <div class="detail-list">
        ${Object.entries(row)
          .filter(([, value]) => typeof value !== "object")
          .map(([key, value]) => `
            <div class="detail-card ${String(value).length > 35 ? "full" : ""}">
              <label>${prettyLabel(key)}</label>
              <div>${formatDetail(value)}</div>
            </div>
          `).join("")}
      </div>
    `;

    drawerBackdrop.classList.remove("hidden");
  } catch (error) {
    toast(error.message, "error");
  }
}

document.getElementById("drawerCloseButton").addEventListener("click", closeDrawer);
drawerBackdrop.addEventListener("click", e => {
  if (e.target === drawerBackdrop) closeDrawer();
});

function closeDrawer() {
  drawerBackdrop.classList.add("hidden");
}

// ------------------------------------------------------------
// Create / Edit modal
// ------------------------------------------------------------
document.getElementById("modalCloseButton").addEventListener("click", closeModal);
document.getElementById("modalCancelButton").addEventListener("click", closeModal);
modalBackdrop.addEventListener("click", e => {
  if (e.target === modalBackdrop) closeModal();
});

async function openEditor(resourceKey, mode, id = null) {
  const config = resources[resourceKey];
  state.modal = { resourceKey, mode, id };

  document.getElementById("modalTitle").textContent =
    mode === "create" ? `Add ${config.singular}` : `Edit ${config.singular}`;

  document.getElementById("modalSubtitle").textContent =
    mode === "create"
      ? `Create a new ${config.singular.toLowerCase()} record.`
      : `Update the selected ${config.singular.toLowerCase()}.`;

  document.getElementById("modalSaveButton").textContent =
    mode === "create" ? "Create" : "Save Changes";

  document.getElementById("formFields").innerHTML =
    `<div class="loading"><span class="spinner"></span>Preparing form...</div>`;

  modalBackdrop.classList.remove("hidden");

  let current = {};

  if (mode === "edit") {
    try {
      const result = await api(config.detailsRoute(id));
      current = result.data || {};

      if (config.transformEditData) {
        current = { ...current, ...config.transformEditData(current) };
      }

      // TeacherDTO only returns FullName/DepartmentName, so recover fields
      // that UpdateTeacherDTO needs from GET all when possible.
      if (resourceKey === "teachers") {
        const cached = (state.data.teachers || []).find(x => x.id === id);
        if (cached) current = { ...cached, ...current };

        current = { ...current, ...splitFullName(current) };

        // TeacherDTO now includes DepartmentId, PhoneNumber and Salary.
        // Keep name matching only as a fallback for older API responses.
        if (!current.departmentId && current.departmentName) {
          const departments = await getList("departments");
          const match = departments.find(d => d.name === current.departmentName);
          if (match) current.departmentId = match.id;
        }
      }

      // StudentDTO returns ClassRoomName, so recover ClassRoomId for the edit dropdown.
      if (resourceKey === "students") {
        current = { ...current, ...splitFullName(current) };

        const classrooms = await getList("classrooms");
        const match = classrooms.find(c => c.name === current.classRoomName);
        if (match) current.classRoomId = match.id;
      }

      // SubjectDTO returns TeacherName, so recover TeacherId for the edit dropdown.
      if (resourceKey === "subjects") {
        // SubjectDTO now includes MaxGrade, so the edit form can prefill it.
        // SubjectDTO still exposes TeacherName, so recover TeacherId for the dropdown.
        if (!current.teacherId && current.teacherName) {
          const teachers = await getList("teachers");
          const match = teachers.find(t => t.fullName === current.teacherName);
          if (match) current.teacherId = match.id;
        }
      }

      // EnrollmentDTO has no SubjectId, so recover it by matching subject name.
      if (resourceKey === "enrollments") {
        const subjects = await getList("subjects");
        const subject = subjects.find(s => s.name === current.subjectName);
        if (subject) current.subjectId = subject.id;
      }
    } catch (error) {
      closeModal();
      toast(error.message, "error");
      return;
    }
  }

  const fields = mode === "create" ? config.createFields : config.updateFields;
  const html = [];

  for (const field of fields) {
    if (field.type === "note") {
      html.push(`
        <div class="form-group ${field.full ? "full" : ""}">
          <label>${field.label}</label>
          <div class="field-note">${escapeHtml(field.text)}</div>
        </div>
      `);
      continue;
    }

    let options = [];

    if (field.optionsFrom) {
      try {
        options = await getList(field.optionsFrom);
      } catch {
        options = [];
      }
    }

    html.push(renderField(field, current[field.key] ?? "", options));
  }

  document.getElementById("formFields").innerHTML = html.join("");
}

function renderField(field, value, options = []) {
  const classes = `form-group ${field.full ? "full" : ""}`;
  const required = field.required ? "required" : "";

  if (field.type === "textarea") {
    return `
      <div class="${classes}">
        <label>${field.label}</label>
        <textarea name="${field.key}" ${required}>${escapeHtml(value)}</textarea>
      </div>
    `;
  }

  if (field.type === "select") {
    return `
      <div class="${classes}">
        <label>${field.label}</label>
        <select name="${field.key}" ${required}>
          <option value="">Select ${field.label}</option>
          ${options.map(item => {
            const optionValue = item[field.optionValue];
            const optionLabel = item[field.optionLabel] ?? item.name ?? item.fullName ?? `#${item.id}`;
            const selected = String(optionValue) === String(value) ? "selected" : "";

            return `
              <option value="${escapeAttr(optionValue)}" ${selected}>
                ${escapeHtml(optionLabel)}
              </option>
            `;
          }).join("")}
        </select>
      </div>
    `;
  }

  return `
    <div class="${classes}">
      <label>${field.label}</label>
      <input
        type="${field.type || "text"}"
        name="${field.key}"
        value="${escapeAttr(value)}"
        ${field.step ? `step="${field.step}"` : ""}
        ${required}
      />
    </div>
  `;
}

entityForm.addEventListener("submit", async e => {
  e.preventDefault();
  if (!state.modal) return;

  const { resourceKey, mode, id } = state.modal;
  const config = resources[resourceKey];
  const fields = mode === "create" ? config.createFields : config.updateFields;
  const formData = new FormData(entityForm);
  const payload = {};

  for (const field of fields) {
    if (field.type === "note") continue;

    let value = formData.get(field.key);

    if (field.type === "number" && value !== "") {
      value = Number(value);
    }

    if (field.type === "select" && value !== "") {
      value = Number(value);
    }

    payload[field.key] = value;
  }

  const saveButton = document.getElementById("modalSaveButton");
  const originalText = saveButton.textContent;
  saveButton.disabled = true;
  saveButton.textContent = "Saving...";

  try {
    if (mode === "create") {
      await api(config.createRoute(), {
        method: "POST",
        body: JSON.stringify(payload)
      });

      toast(`${config.singular} created successfully.`, "success");
    } else {
      await api(config.updateRoute(id), {
        method: "PUT",
        body: JSON.stringify(payload)
      });

      toast(`${config.singular} updated successfully.`, "success");
    }

    invalidate(resourceKey);
    closeModal();

    if (state.page === resourceKey) {
      await loadTable(resourceKey);
    } else {
      await renderDashboard();
    }
  } catch (error) {
    toast(error.message, "error");
  } finally {
    saveButton.disabled = false;
    saveButton.textContent = originalText;
  }
});

function closeModal() {
  modalBackdrop.classList.add("hidden");
  entityForm.reset();
  state.modal = null;
}

// ------------------------------------------------------------
// Delete
// ------------------------------------------------------------
async function deleteRecord(resourceKey, id) {
  const config = resources[resourceKey];

  if (!confirm(`Delete this ${config.singular.toLowerCase()}?`)) return;

  try {
    await api(config.deleteRoute(id), { method: "DELETE" });
    toast(`${config.singular} deleted successfully.`, "success");

    invalidate(resourceKey);
    await loadTable(resourceKey);
  } catch (error) {
    toast(error.message, "error");
  }
}

// ------------------------------------------------------------
// Department search by teacher
// ------------------------------------------------------------
async function searchDepartmentByTeacher() {
  const fullName = prompt("Enter teacher full name:");
  if (!fullName?.trim()) return;

  try {
    const result = await api(`Department/Search?fullName=${encodeURIComponent(fullName.trim())}`);
    const department = result.data || {};

    document.getElementById("drawerTitle").textContent = "Department Found";
    document.getElementById("drawerSubtitle").textContent = `Search: ${fullName}`;

    document.getElementById("drawerBody").innerHTML = `
      <div class="detail-list">
        ${Object.entries(department)
          .filter(([, value]) => typeof value !== "object")
          .map(([key, value]) => `
            <div class="detail-card">
              <label>${prettyLabel(key)}</label>
              <div>${formatDetail(value)}</div>
            </div>
          `).join("")}
      </div>
    `;

    drawerBackdrop.classList.remove("hidden");
  } catch (error) {
    toast(error.message, "error");
  }
}

// ------------------------------------------------------------
// Helpers
// ------------------------------------------------------------
function splitFullName(row) {
  const fullName = row?.fullName || row?.studentFullName || "";
  const parts = String(fullName).trim().split(/\s+/).filter(Boolean);

  if (!parts.length) {
    return { firstName: "", lastName: "" };
  }

  return {
    firstName: parts[0],
    lastName: parts.slice(1).join(" ")
  };
}

function formatDetail(value) {
  if (value === null || value === undefined || value === "") return "—";

  if (typeof value === "string" && /^\d{4}-\d{2}-\d{2}T/.test(value)) {
    const date = new Date(value);
    if (!Number.isNaN(date.getTime())) return escapeHtml(date.toLocaleString());
  }

  return escapeHtml(String(value));
}

function prettyLabel(key) {
  return String(key)
    .replace(/([A-Z])/g, " $1")
    .replace(/^./, s => s.toUpperCase());
}

function setApiStatus(online) {
  const dot = document.getElementById("apiDot");
  const label = document.getElementById("apiStatus");

  dot.classList.remove("online", "offline");
  dot.classList.add(online ? "online" : "offline");
  label.textContent = online ? "API Connected" : "API Offline";
}

async function checkApi() {
  try {
    await api("Department");
    setApiStatus(true);
  } catch {
    setApiStatus(false);
  }
}

function toast(message, type = "") {
  const container = document.getElementById("toastContainer");
  const element = document.createElement("div");

  element.className = `toast ${type}`;
  element.textContent = message;

  container.appendChild(element);
  setTimeout(() => element.remove(), 3500);
}

function escapeHtml(value) {
  return String(value ?? "")
    .replaceAll("&", "&amp;")
    .replaceAll("<", "&lt;")
    .replaceAll(">", "&gt;")
    .replaceAll('"', "&quot;")
    .replaceAll("'", "&#039;");
}

function escapeAttr(value) {
  return escapeHtml(value).replaceAll("`", "&#096;");
}

// ------------------------------------------------------------
// Start
// ------------------------------------------------------------
checkApi();
navigate("dashboard");
