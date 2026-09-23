// ============================================================
// SCHOOL SYSTEM FRONTEND
// Change only this value if your ASP.NET API uses another port.
// ============================================================
const API_BASE = "http://localhost:5203/api";

// The first 3 schemas are exact from your current controllers/DTOs.
// Student / Subject / Enrollment fields are easy to adjust here if
// your DTO property names are different.
const resources = {
  departments: {
    title: "Departments",
    singular: "Department",
    endpoint: "Department",
    subtitle: "Manage academic departments and search by teacher name.",
    columns: [
      { key: "id", label: "ID" },
      { key: "name", label: "Name" },
      { key: "description", label: "Description" }
    ],
    fields: [
      { key: "name", label: "Name", type: "text", required: true },
      { key: "description", label: "Description", type: "textarea", full: true }
    ],
    updateRoute: (id) => `Department/${id}`,
    deleteRoute: (id) => `Department?id=${id}`
  },

  classrooms: {
    title: "Classrooms",
    singular: "Classroom",
    endpoint: "ClassRooms",
    subtitle: "Manage classes, grade levels and room capacity.",
    columns: [
      { key: "id", label: "ID" },
      { key: "name", label: "Name" },
      { key: "gradeLevel", label: "Grade Level" },
      { key: "capacity", label: "Capacity" }
    ],
    fields: [
      { key: "name", label: "Name", type: "text", required: true },
      { key: "gradeLevel", label: "Grade Level", type: "text", required: true },
      { key: "capacity", label: "Capacity", type: "number", required: true }
    ],
    updateRoute: (id) => `ClassRooms/${id}`,
    deleteRoute: (id) => `ClassRooms?id=${id}`
  },

  teachers: {
    title: "Teachers",
    singular: "Teacher",
    endpoint: "Teacher",
    subtitle: "Manage teachers and their department assignments.",
    columns: [
      { key: "id", label: "ID" },
      { key: "fullName", label: "Teacher" },
      { key: "email", label: "Email" },
      { key: "departmentName", label: "Department" }
    ],
    fields: [
      { key: "firstName", label: "First Name", type: "text", required: true },
      { key: "lastName", label: "Last Name", type: "text", required: true },
      { key: "email", label: "Email", type: "email", required: true },
      { key: "phoneNumber", label: "Phone Number", type: "text" },
      { key: "salary", label: "Salary", type: "number", required: true },
      {
        key: "departmentId",
        label: "Department",
        type: "select",
        required: true,
        optionsFrom: "departments",
        optionValue: "id",
        optionLabel: "name"
      }
    ],
    updateRoute: (id) => `Teacher?id=${id}`,
    deleteRoute: (id) => `Teacher?id=${id}`
  },

  students: {
    title: "Students",
    singular: "Student",
    endpoint: "Student",
    subtitle: "Manage student records and classroom assignments.",
    columns: [
      { key: "id", label: "ID" },
      { key: "fullName", label: "Student" },
      { key: "name", label: "Name" },
      { key: "age", label: "Age" },
      { key: "email", label: "Email" },
      { key: "classRoomName", label: "Classroom" }
    ],
    fields: [
      { key: "firstName", label: "First Name", type: "text" },
      { key: "lastName", label: "Last Name", type: "text" },
      { key: "name", label: "Name (if your DTO uses Name)", type: "text" },
      { key: "age", label: "Age", type: "number" },
      { key: "email", label: "Email", type: "email" },
      {
        key: "classRoomId",
        label: "Classroom",
        type: "select",
        optionsFrom: "classrooms",
        optionValue: "id",
        optionLabel: "name"
      }
    ],
    updateRoute: () => "Student",
    deleteRoute: (id) => `Student?id=${id}`,
    putUsesBodyId: true
  },

  subjects: {
    title: "Subjects",
    singular: "Subject",
    endpoint: "Subject",
    subtitle: "Manage subjects and teacher assignments.",
    columns: [
      { key: "id", label: "ID" },
      { key: "name", label: "Subject" },
      { key: "title", label: "Title" },
      { key: "teacherName", label: "Teacher" }
    ],
    fields: [
      { key: "name", label: "Name", type: "text" },
      { key: "title", label: "Title (if your DTO uses Title)", type: "text" },
      {
        key: "teacherId",
        label: "Teacher",
        type: "select",
        optionsFrom: "teachers",
        optionValue: "id",
        optionLabel: "fullName"
      }
    ],
    updateRoute: () => "Subject",
    deleteRoute: (id) => `Subject?id=${id}`,
    putUsesBodyId: true
  },

  enrollments: {
    title: "Enrollments",
    singular: "Enrollment",
    endpoint: "Enrollment",
    subtitle: "Manage student subject registrations and grades.",
    columns: [
      { key: "id", label: "ID" },
      { key: "studentName", label: "Student" },
      { key: "subjectName", label: "Subject" },
      { key: "grade", label: "Grade" }
    ],
    fields: [
      {
        key: "studentId",
        label: "Student",
        type: "select",
        required: true,
        optionsFrom: "students",
        optionValue: "id",
        optionLabel: "fullName"
      },
      {
        key: "subjectId",
        label: "Subject",
        type: "select",
        required: true,
        optionsFrom: "subjects",
        optionValue: "id",
        optionLabel: "name"
      },
      { key: "grade", label: "Grade", type: "number" }
    ],
    updateRoute: (id) => `Enrollment?id=${id}`,
    deleteRoute: (id) => `Enrollment?id=${id}`
  }
};

const state = {
  page: "dashboard",
  data: {},
  modal: null,
  search: ""
};

const pageRoot = document.getElementById("pageRoot");
const pageTitle = document.getElementById("pageTitle");
const pageSubtitle = document.getElementById("pageSubtitle");
const sidebar = document.getElementById("sidebar");

document.getElementById("apiBaseLabel").textContent = API_BASE;
document.getElementById("todayLabel").textContent = new Intl.DateTimeFormat("en", {
  weekday: "short",
  year: "numeric",
  month: "short",
  day: "numeric"
}).format(new Date());

// ------------------------------------------------------------
// API helpers
// ------------------------------------------------------------
async function api(path, options = {}) {
  const url = `${API_BASE}/${path}`;
  const config = { ...options };

  config.headers = {
    ...(options.body ? { "Content-Type": "application/json" } : {}),
    ...(options.headers || {})
  };

  const response = await fetch(url, config);

  if (response.status === 204) {
    return { ok: true, status: 204, data: null };
  }

  const text = await response.text();
  let data = null;

  if (text) {
    try { data = JSON.parse(text); }
    catch { data = text; }
  }

  if (!response.ok) {
    const message = typeof data === "string"
      ? data
      : data?.title || data?.message || `Request failed (${response.status})`;

    const error = new Error(message);
    error.status = response.status;
    error.data = data;
    throw error;
  }

  return { ok: true, status: response.status, data };
}

async function safeList(resourceKey, force = false) {
  if (!force && Array.isArray(state.data[resourceKey])) {
    return state.data[resourceKey];
  }

  const config = resources[resourceKey];
  const result = await api(config.endpoint);
  state.data[resourceKey] = Array.isArray(result.data) ? result.data : [];
  return state.data[resourceKey];
}

function invalidate(resourceKey) {
  delete state.data[resourceKey];
}

// ------------------------------------------------------------
// Navigation
// ------------------------------------------------------------
document.getElementById("nav").addEventListener("click", (e) => {
  const btn = e.target.closest(".nav-item");
  if (!btn) return;

  navigate(btn.dataset.page);
});

document.getElementById("menuBtn").addEventListener("click", () => {
  sidebar.classList.toggle("open");
});

document.getElementById("refreshBtn").addEventListener("click", () => {
  if (state.page === "dashboard") {
    Object.keys(resources).forEach(invalidate);
    renderDashboard();
  } else {
    invalidate(state.page);
    renderResourcePage(state.page);
  }
});

function navigate(page) {
  state.page = page;
  state.search = "";

  document.querySelectorAll(".nav-item").forEach(btn => {
    btn.classList.toggle("active", btn.dataset.page === page);
  });

  sidebar.classList.remove("open");

  if (page === "dashboard") {
    pageTitle.textContent = "Dashboard";
    pageSubtitle.textContent = "Overview of your school";
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
    <div class="hero">
      <div>
        <h2>Welcome to School System</h2>
        <p>Manage departments, classrooms, teachers, students, subjects and enrollments from one dashboard.</p>
      </div>
      <div class="hero-badge">SS</div>
    </div>

    <div class="stats-grid" id="statsGrid">
      ${["Departments", "Classrooms", "Teachers", "Students"].map(label => `
        <div class="stat-card">
          <div class="stat-card-top">
            <span class="stat-label">${label}</span>
            <span class="stat-icon">••</span>
          </div>
          <div class="stat-value">—</div>
        </div>
      `).join("")}
    </div>

    <div class="dashboard-grid">
      <div class="panel">
        <div class="panel-head">
          <div>
            <h3>Quick Actions</h3>
            <p>Create common school records quickly</p>
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
              <span>Add a teacher and department</span>
            </button>
            <button class="quick-action" data-create="classrooms">
              <strong>Add Classroom</strong>
              <span>Create a class and set capacity</span>
            </button>
            <button class="quick-action" data-create="enrollments">
              <strong>New Enrollment</strong>
              <span>Assign a student to a subject</span>
            </button>
          </div>
        </div>
      </div>

      <div class="panel">
        <div class="panel-head">
          <div>
            <h3>System Summary</h3>
            <p>Additional academic records</p>
          </div>
        </div>
        <div class="panel-body">
          <div class="activity-list" id="summaryList">
            <div class="loading-row"><span class="spinner"></span> Loading summary...</div>
          </div>
        </div>
      </div>
    </div>
  `;

  pageRoot.querySelectorAll("[data-create]").forEach(btn => {
    btn.addEventListener("click", () => openModal(btn.dataset.create, "create"));
  });

  const keys = ["departments", "classrooms", "teachers", "students", "subjects", "enrollments"];
  const results = await Promise.allSettled(keys.map(k => safeList(k, true)));

  const counts = {};
  keys.forEach((key, index) => {
    counts[key] = results[index].status === "fulfilled" ? results[index].value.length : null;
  });

  const statLabels = [
    ["departments", "Departments", "D"],
    ["classrooms", "Classrooms", "C"],
    ["teachers", "Teachers", "T"],
    ["students", "Students", "S"]
  ];

  document.getElementById("statsGrid").innerHTML = statLabels.map(([key, label, icon]) => `
    <div class="stat-card">
      <div class="stat-card-top">
        <span class="stat-label">${label}</span>
        <span class="stat-icon">${icon}</span>
      </div>
      <div class="stat-value">${counts[key] ?? "!"}</div>
    </div>
  `).join("");

  document.getElementById("summaryList").innerHTML = `
    ${summaryRow("Subjects", counts.subjects)}
    ${summaryRow("Enrollments", counts.enrollments)}
    ${summaryRow("API", Object.values(counts).some(v => v === null) ? "Partial" : "Connected")}
  `;

  const allFailed = results.every(x => x.status === "rejected");
  setApiStatus(!allFailed);
}

function summaryRow(label, value) {
  return `
    <div class="activity-row">
      <div>
        <strong>${label}</strong>
        <span>Current system data</span>
      </div>
      <span class="badge">${value ?? "Unavailable"}</span>
    </div>
  `;
}

// ------------------------------------------------------------
// Resource pages
// ------------------------------------------------------------
async function renderResourcePage(resourceKey) {
  const config = resources[resourceKey];

  pageRoot.innerHTML = `
    <div class="toolbar">
      <div class="toolbar-left">
        <div class="search-box">
          <span>⌕</span>
          <input id="tableSearch" type="search" placeholder="Search ${config.title.toLowerCase()}..." />
        </div>
        ${resourceKey === "departments" ? `
          <button class="btn ghost" id="teacherSearchBtn">Search by teacher</button>
        ` : ""}
      </div>

      <div class="toolbar-right">
        <button class="btn secondary" id="reloadBtn">Refresh</button>
        <button class="btn primary" id="createBtn">+ Add ${config.singular}</button>
      </div>
    </div>

    <div class="table-card">
      <div id="tableHost">
        <div class="loading-row"><span class="spinner"></span> Loading ${config.title.toLowerCase()}...</div>
      </div>
    </div>
  `;

  document.getElementById("createBtn").addEventListener("click", () => openModal(resourceKey, "create"));
  document.getElementById("reloadBtn").addEventListener("click", async () => {
    invalidate(resourceKey);
    await loadResourceTable(resourceKey);
  });

  document.getElementById("tableSearch").addEventListener("input", (e) => {
    state.search = e.target.value.trim().toLowerCase();
    paintTable(resourceKey);
  });

  if (resourceKey === "departments") {
    document.getElementById("teacherSearchBtn").addEventListener("click", searchDepartmentByTeacher);
  }

  await loadResourceTable(resourceKey);
}

async function loadResourceTable(resourceKey) {
  const host = document.getElementById("tableHost");
  if (!host) return;

  host.innerHTML = `<div class="loading-row"><span class="spinner"></span> Loading...</div>`;

  try {
    await safeList(resourceKey, true);
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
  const rows = (state.data[resourceKey] || []).filter(row => {
    if (!state.search) return true;
    return Object.values(row || {}).some(value =>
      String(value ?? "").toLowerCase().includes(state.search)
    );
  });

  if (!rows.length) {
    host.innerHTML = `
      <div class="empty">
        <strong>No ${config.title.toLowerCase()} found</strong>
        <span>Add a ${config.singular.toLowerCase()} or change your search.</span>
      </div>
    `;
    return;
  }

  const visibleColumns = config.columns.filter(col =>
    rows.some(row => hasUsefulValue(row[col.key]))
  );

  const finalColumns = visibleColumns.length ? visibleColumns : config.columns.slice(0, 3);

  host.innerHTML = `
    <div class="table-wrap">
      <table>
        <thead>
          <tr>
            ${finalColumns.map(c => `<th>${c.label}</th>`).join("")}
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          ${rows.map(row => `
            <tr>
              ${finalColumns.map(c => `<td>${formatValue(row[c.key])}</td>`).join("")}
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

  host.querySelectorAll("[data-view]").forEach(btn => {
    btn.addEventListener("click", () => viewRecord(resourceKey, btn.dataset.view));
  });

  host.querySelectorAll("[data-edit]").forEach(btn => {
    btn.addEventListener("click", () => openModal(resourceKey, "edit", Number(btn.dataset.edit)));
  });

  host.querySelectorAll("[data-delete]").forEach(btn => {
    btn.addEventListener("click", () => deleteRecord(resourceKey, Number(btn.dataset.delete)));
  });
}

function hasUsefulValue(value) {
  return value !== undefined && value !== null && value !== "";
}

function formatValue(value) {
  if (value === null || value === undefined || value === "") return `<span class="muted">—</span>`;
  if (typeof value === "boolean") return value ? "Yes" : "No";
  if (typeof value === "object") return `<span class="muted">Object</span>`;
  return escapeHtml(String(value));
}

async function viewRecord(resourceKey, id) {
  const config = resources[resourceKey];

  try {
    const result = await api(`${config.endpoint}/${id}`);
    const row = result.data;

    if (!row || typeof row !== "object") {
      toast("Record loaded but has no displayable data.", "error");
      return;
    }

    const fields = Object.entries(row).filter(([,v]) => typeof v !== "object");
    openInfoModal(`${config.singular} Details`, fields);
  } catch (error) {
    toast(error.message, "error");
  }
}

async function searchDepartmentByTeacher() {
  const fullName = prompt("Enter teacher full name:");
  if (!fullName?.trim()) return;

  try {
    const result = await api(`Department/Search?fullName=${encodeURIComponent(fullName.trim())}`);
    const d = result.data;

    openInfoModal("Department Found", Object.entries(d || {}).filter(([,v]) => typeof v !== "object"));
  } catch (error) {
    toast(error.message, "error");
  }
}

// ------------------------------------------------------------
// Modal / forms
// ------------------------------------------------------------
const modalBackdrop = document.getElementById("modalBackdrop");
const form = document.getElementById("entityForm");

document.getElementById("closeModalBtn").addEventListener("click", closeModal);
document.getElementById("cancelModalBtn").addEventListener("click", closeModal);
modalBackdrop.addEventListener("click", (e) => {
  if (e.target === modalBackdrop) closeModal();
});

async function openModal(resourceKey, mode, id = null) {
  const config = resources[resourceKey];

  state.modal = { resourceKey, mode, id };
  document.getElementById("modalTitle").textContent =
    mode === "create" ? `Add ${config.singular}` : `Edit ${config.singular}`;

  document.getElementById("modalHint").textContent =
    mode === "create"
      ? `Create a new ${config.singular.toLowerCase()} record.`
      : `Update the selected ${config.singular.toLowerCase()}.`;

  document.getElementById("saveEntityBtn").textContent =
    mode === "create" ? "Create" : "Save Changes";

  document.getElementById("formFields").innerHTML =
    `<div class="loading-row"><span class="spinner"></span> Preparing form...</div>`;

  modalBackdrop.classList.remove("hidden");

  let current = {};
  if (mode === "edit") {
    try {
      const result = await api(`${config.endpoint}/${id}`);
      current = result.data || {};
    } catch (error) {
      closeModal();
      toast(error.message, "error");
      return;
    }
  }

  const html = [];

  for (const field of config.fields) {
    let options = [];

    if (field.optionsFrom) {
      try {
        options = await safeList(field.optionsFrom);
      } catch {
        options = [];
      }
    }

    const value = current[field.key] ?? findFallbackValue(current, field.key) ?? "";

    html.push(renderField(field, value, options));
  }

  document.getElementById("formFields").innerHTML = html.join("");
}

function findFallbackValue(current, key) {
  const aliases = {
    firstName: ["firstName"],
    lastName: ["lastName"],
    classRoomId: ["classRoomId", "classroomId"],
    teacherId: ["teacherId"],
    studentId: ["studentId"],
    subjectId: ["subjectId"]
  };

  return (aliases[key] || []).map(k => current[k]).find(v => v !== undefined);
}

function renderField(field, value, options) {
  const classes = `form-group ${field.full ? "full" : ""}`;
  const required = field.required ? "required" : "";

  if (field.type === "textarea") {
    return `
      <div class="${classes}">
        <label>${field.label}</label>
        <textarea name="${field.key}" ${required}>${escapeHtml(String(value ?? ""))}</textarea>
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
            const label = buildOptionLabel(item, field.optionLabel);
            const selected = String(optionValue) === String(value) ? "selected" : "";
            return `<option value="${escapeAttr(optionValue)}" ${selected}>${escapeHtml(label)}</option>`;
          }).join("")}
        </select>
      </div>
    `;
  }

  return `
    <div class="${classes}">
      <label>${field.label}</label>
      <input
        name="${field.key}"
        type="${field.type || "text"}"
        value="${escapeAttr(value)}"
        ${required}
      />
    </div>
  `;
}

function buildOptionLabel(item, preferred) {
  if (preferred && hasUsefulValue(item[preferred])) return String(item[preferred]);
  if (hasUsefulValue(item.fullName)) return String(item.fullName);
  if (hasUsefulValue(item.name)) return String(item.name);
  if (hasUsefulValue(item.title)) return String(item.title);
  return `#${item.id ?? "?"}`;
}

form.addEventListener("submit", async (e) => {
  e.preventDefault();
  if (!state.modal) return;

  const { resourceKey, mode, id } = state.modal;
  const config = resources[resourceKey];
  const fd = new FormData(form);
  const payload = {};

  for (const field of config.fields) {
    let value = fd.get(field.key);

    // Don't send blank optional fields. This makes the fallback schemas
    // safer for DTOs that don't contain every optional property.
    if (value === "" && !field.required) continue;

    if (field.type === "number" && value !== "") value = Number(value);
    if (field.type === "select" && value !== "") value = Number(value);

    payload[field.key] = value;
  }

  if (mode === "edit" && config.putUsesBodyId) {
    payload.id = id;
  }

  const saveBtn = document.getElementById("saveEntityBtn");
  const originalText = saveBtn.textContent;
  saveBtn.disabled = true;
  saveBtn.textContent = "Saving...";

  try {
    if (mode === "create") {
      await api(config.endpoint, {
        method: "POST",
        body: JSON.stringify(payload)
      });
      toast(`${config.singular} created successfully.`, "success");
    } else {
      const route = config.updateRoute(id);
      await api(route, {
        method: "PUT",
        body: JSON.stringify(payload)
      });
      toast(`${config.singular} updated successfully.`, "success");
    }

    invalidate(resourceKey);
    closeModal();

    if (state.page === resourceKey) {
      await loadResourceTable(resourceKey);
    } else {
      renderDashboard();
    }
  } catch (error) {
    toast(error.message, "error");
  } finally {
    saveBtn.disabled = false;
    saveBtn.textContent = originalText;
  }
});

function closeModal() {
  modalBackdrop.classList.add("hidden");
  state.modal = null;
  form.reset();
}

function openInfoModal(title, fields) {
  state.modal = { info: true };
  document.getElementById("modalTitle").textContent = title;
  document.getElementById("modalHint").textContent = "Record details";
  document.getElementById("saveEntityBtn").classList.add("hidden");

  document.getElementById("formFields").innerHTML = fields.map(([key, value]) => `
    <div class="form-group">
      <label>${prettyLabel(key)}</label>
      <input value="${escapeAttr(value)}" disabled />
    </div>
  `).join("");

  modalBackdrop.classList.remove("hidden");

  const restore = () => {
    document.getElementById("saveEntityBtn").classList.remove("hidden");
  };

  const observer = new MutationObserver(() => {
    if (modalBackdrop.classList.contains("hidden")) {
      restore();
      observer.disconnect();
    }
  });

  observer.observe(modalBackdrop, { attributes: true });
}

// ------------------------------------------------------------
// CRUD delete
// ------------------------------------------------------------
async function deleteRecord(resourceKey, id) {
  const config = resources[resourceKey];

  if (!confirm(`Delete this ${config.singular.toLowerCase()}?`)) return;

  try {
    await api(config.deleteRoute(id), { method: "DELETE" });
    toast(`${config.singular} deleted successfully.`, "success");

    invalidate(resourceKey);
    await loadResourceTable(resourceKey);
  } catch (error) {
    toast(error.message, "error");
  }
}

// ------------------------------------------------------------
// Status / toasts / formatting
// ------------------------------------------------------------
async function checkApi() {
  try {
    await api("Department");
    setApiStatus(true);
  } catch {
    setApiStatus(false);
  }
}

function setApiStatus(online) {
  const dot = document.getElementById("apiDot");
  const label = document.getElementById("apiStatusText");

  dot.classList.remove("online", "offline");
  dot.classList.add(online ? "online" : "offline");
  label.textContent = online ? "API Connected" : "API Offline";
}

function toast(message, type = "") {
  const container = document.getElementById("toastContainer");
  const el = document.createElement("div");
  el.className = `toast ${type}`;
  el.textContent = message;
  container.appendChild(el);

  setTimeout(() => el.remove(), 3500);
}

function prettyLabel(key) {
  return key
    .replace(/([A-Z])/g, " $1")
    .replace(/^./, s => s.toUpperCase());
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

// Start
checkApi();
navigate("dashboard");
