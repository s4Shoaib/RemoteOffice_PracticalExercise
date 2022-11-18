<template>
    <v-app light="">
        <div id="appointmentSearch">
            <div class="row">
                <div class="col-xs-6">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-xs-3 control-label">
                                From Date
                            </label>
                            <div class="col-xs-5">
                                <v-flex>
                                    <v-menu :close-on-content-click="false"
                                            v-model="fromDateState"
                                            :nudge-right="40"
                                            lazy=""
                                            transition="scale-transition"
                                            offset-y=""
                                            full-width=""
                                            max-width="290px"
                                            min-width="290px">
                                        <v-text-field slot="activator"
                                                      v-model="fromDateComputed"
                                                      hint="MM/DD/YYYY format"
                                                      persistent-hint=""
                                                      prepend-icon="event"
                                                      @blur="fromDate = parseDate(fromDateComputed)"></v-text-field>
                                        <v-date-picker v-model="fromDate" no-title="" @input="fromDateState = false"></v-date-picker>
                                    </v-menu>
                                </v-flex>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-xs-3 control-label">
                                To Date
                            </label>
                            <div class="col-xs-5">
                                <v-flex>
                                    <v-menu :close-on-content-click="false"
                                            v-model="toDateState"
                                            :nudge-right="40"
                                            lazy=""
                                            transition="scale-transition"
                                            offset-y=""
                                            full-width=""
                                            max-width="290px"
                                            min-width="290px">
                                        <v-text-field slot="activator"
                                                      v-model="toDateComputed"
                                                      hint="MM/DD/YYYY format"
                                                      persistent-hint=""
                                                      prepend-icon="event"
                                                      @blur="toDate = parseDate(toDateComputed)"
                                                      :rules="toDateValidationRules"></v-text-field>
                                        <v-date-picker v-model="toDate" no-title="" @input="toDateState = false"></v-date-picker>
                                    </v-menu>
                                </v-flex>
                            </div>
                        </div>
                        <div>
                            <v-btn small="" color="v-button" @click.native="search">Search</v-btn>
                        </div>
                    </div>
                </div>
                <div class="col-xs-3 pull-right buttonAlignRight">
                    <v-btn small="" color="v-button" @click.native="reset">Reset</v-btn>
                </div>
            </div>
            <div class="gridContainer">
                <template>
                    <v-data-table v-model="selected"
                                  :headers="headers"
                                  :items="practitionerProfitabilitys"
                                  :pagination.sync="pagination"
                                  select-all=""
                                  item-key="practitioner_Id"
                                  class="elevation-1">

                        <template slot="headers" slot-scope="props">
                            <tr>
                                <th v-for="header in props.headers"
                                    :key="header.text"
                                    :class="['column sortable', pagination.descending ? 'desc' : 'asc', header.value === pagination.sortBy ? 'active' : '']"
                                    :width="header.width"
                                    @click="changeSort(header.value)">
                                    <v-icon small="">arrow_upward</v-icon>
                                    {{ header.text }}
                                </th>
                            </tr>
                        </template>
                        <template slot="items" slot-scope="props">
                            <tr :active="props.selected">
                                <td @click="openPractitionerAppointmentsDetails(props.item.practitioner_Id)" style="vertical-align:top">{{ props.item.practitioner_Name }}</td>
                                <td colspan="3">
                                    <div v-for='(m, groupIndex) in props.item.monthlyProfitability'>
                                <td style="width:50%">{{ m.monthYear }}</td>
                                <td  style="width:50%">{{ m.cost }}</td>
                                <td>{{ m.revenue }}</td>
            </div>
            </td>
            </tr>
</template>
<template slot="no-data">
    <v-alert :value="showGridAlert" color="error" icon="warning" outline="">
        No data found for your search
    </v-alert>
</template>
                    </v-data-table>
                </template>
            </div>
<v-dialog v-model="practitionerAppointmentDialog">
    <v-card>
        <v-form ref="form" lazy-validation="">
            <v-card-title class="headlineTitle">
                <span class="headline">{{ practitionerAppointmentDialogTitle }}</span>
            </v-card-title>
            <div class="gridContainer">
                <template>
                    <v-data-table :headers="appointmentHeaders"
                                  :items="practitionerAppointments"
                                  :pagination.sync="pagination"
                                  item-key="id"
                                  class="elevation-1">

                        <template slot="appointmentHeaders" slot-scope="props">
                            <tr>
                                <th v-for="header in props.appointmentHeaders"
                                    :key="header.text"
                                    :class="['column sortable', pagination.descending ? 'desc' : 'asc', header.value === pagination.sortBy ? 'practitioner_Name' : '']"
                                    :width="header.width"
                                    @click="changeSort(header.value)">
                                    <v-icon small="">arrow_upward</v-icon>
                                    {{ header.text }}
                                </th>
                            </tr>
                        </template>
                        <template slot="items" slot-scope="props">
                            <tr>
                                <td>{{ props.item.practitioner_Name }}</td>
                                <td>{{ props.item.client_Name }}</td>
                                <td>{{ props.item.appointment_Type }}</td>
                                <td>{{ props.item.cost }}</td>
                                <td>{{ props.item.revenue }}</td>
                                <td>{{ props.item.duration }}</td>
                                <td>{{ props.item.dateString }}</td>
                            </tr>
                        </template>
                        <template slot="no-data">
                            <v-alert :value="showGridAlert" color="error" icon="warning" outline="">
                                No appointmnet found for the practitioner with the passed date range.
                            </v-alert>
                        </template>
                    </v-data-table>
                </template>
            </div>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="indigo darken-1" outline="" flat="" @click.native="close">Close</v-btn>
            </v-card-actions>
        </v-form>
    </v-card>
</v-dialog>
        </div>
    </v-app>
</template>
<script src="./Search.js"></script>
