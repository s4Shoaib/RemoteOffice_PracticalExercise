//Register all the Vue components here for easy maintenance
import Vue from 'vue';

import 'babel-polyfill';

import VeeValidate from 'vee-validate';
Vue.use(VeeValidate);

import Vuetify from 'vuetify';
Vue.use(Vuetify);

import PractitionerAppointmentSearch from './PractitionerAppointment/Search.vue';

if (document.getElementById("appointmentSearch")) {
    new Vue({
        el: '#appointmentSearch',
        template: '<PractitionerAppointmentSearch/>',
        components: { PractitionerAppointmentSearch }
    });
}
