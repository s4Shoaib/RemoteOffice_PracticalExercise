import moment from 'moment'
import axios from 'axios'
import Search from './Search.vue'
import commonJS from '../common.js'

export default {
    components: {
        Search
    },
    data: function () {
        return {
            pageName: 'AppointmentSearch',
            fromDate: '',
            fromDateState: false,
            fromDateComputed: '',
            toDateState: false,
            toDateComputed: '',
            toDate: '',
            practitionerAppointmentDialog: false,
            practitionerAppointmentDialogTitle: '',
            //Grid data////////////////////////////////////////////////////////////////////////////////////////////////
            showGridAlert: false,
            pagination: {
                page: 1,
                rowsPerPage: 10,
                sortBy: 'practitioner_Name'
            },
            selected: [],
            headers: [
                {
                    text: 'Practitioner_Name',
                    align: 'left',
                    value: 'practitioner_Name',
                    width: '30%'
                },
                { text: 'Month', value: 'month', width: '30%' },
                { text: 'Cost', value: 'cost', width: '20%' },
                { text: 'Revenue', value: 'revenue', width: '20%' }
            ],
            appointmentHeaders: [
                {
                    text: 'Practitioner_Name',
                    align: 'left',
                    value: 'practitioner_Name',
                    width: '30%'
                },
                { text: 'Client_Name', value: 'client_Name', width: '30%' },
                { text: 'Appointment_Type', value: 'appointment_Type', width: '10%' },
                { text: 'Cost', value: 'cost', width: '5%' },
                { text: 'Revenue', value: 'revenue', width: '5%' },
                { text: 'Duration', value: 'duration', width: '5%' },
                { text: 'Date', value: 'month', width: '15%' }
            ],
            practitionerProfitabilitys: [],
            practitionerAppointments: [],
            toDateValidationRules: [
                (v) => this.validateDateFormat(v) != false || 'Invalid date',
                (v) => this.validateDate() != false || 'To Date should be greater than From Date'
            ]
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    },
    watch: {
        fromDate: function () {
            this.fromDateComputed = moment(this.fromDate).isValid() ? moment(this.fromDate).format('MM/DD/YYYY') : '';
        },
        toDate: function () {
            this.toDateComputed = moment(this.toDate).isValid() ? moment(this.toDate).format('MM/DD/YYYY') : '';
        }
    },
    methods: {
        init() {
            this.getPractitionerProfitabilitys();
        },
        getPractitionerProfitabilitys() {
            var self = this;

            axios.get('/api/Appointment/Get?' + 'fromDate=' + this.fromDate + '&toDate=' + this.toDate)
                .then((response) => {
                    self.practitionerProfitabilitys = response.data.practitionerProfitabilitys;
                    self.pagination.page = 1;
                    self.showGridAlert = true;
                })
                .catch((error) => {
                    console.log(error);
                    commonJS.handleAjaxError(error);
                })
        },
        openPractitionerAppointmentsDetails(practitionerId) {
            this.practitionerAppointmentDialog = true;
            this.practitionerAppointmentDialogTitle = 'Practitioner Appointments';
            this.getPractitionerAppointments(practitionerId);
        },
        getPractitionerAppointments(practitionerId) {
            var self = this;

            var appointmentSearch = {
                "practitioner_Id": practitionerId,
                "fromDate": this.fromDate,
                "ToDate": this.toDate
            };

            axios.post('/api/Appointment/Search/', appointmentSearch)
                .then((response) => {
                    self.practitionerAppointments = response.data.appointments;
                    self.pagination.page = 1;
                    self.showGridAlert = true;
                })
                .catch((error) => {
                    console.log(error);
                    commonJS.handleAjaxError(error);
                })
        },
        reset(e) {
            this.fromDate = '';
            this.toDate = '';
            this.pagination.sortBy = "practitioner_Name";
            this.pagination.descending = false;
            this.pagination.rowsPerPage = 10;
            this.getPractitionerProfitabilitys();
        },
        search() {
            if (this.validateDate()) {
                this.getPractitionerProfitabilitys();
            }
        },
        parseDate(date) {
            if (!date) return null;

            const [month, day, year] = date.split('/');
            if (this.validateDateFormat(date)) {
                return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
            }
        },
        validateDateFormat(date) {
            if (typeof date === 'undefined' || date.length === 0) {
                return true;
            } else {
                return moment(date, 'M/D/YYYY', true).isValid() || moment(date, 'MM/DD/YYYY', true).isValid();
            }
        },
        validateDate() {
            var _fromDate = new Date(this.fromDate);
            var _toDate = new Date(this.toDate);
            if (typeof (_toDate) != 'undefined' && typeof (_fromDate) != 'undefined' && _fromDate > _toDate) {
                return false;
            }
            return true;
        },
        close() {
            this.practitionerAppointmentDialog = false;
            if (this.validateDate()) {
                this.getPractitionerProfitabilitys();
            }
        },
        //Grid methods/////////////////////////////////////////////////////////////////
        changeSort(column) {
            if (this.pagination.sortBy === column) {
                this.pagination.descending = !this.pagination.descending
            } else {
                this.pagination.sortBy = column
                this.pagination.descending = false
            }
        }

        ////////////////////////////////////////////////////////////////////////////////
    },
    mounted() {
        this.init();
    }
};
