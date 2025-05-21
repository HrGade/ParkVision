<template>
  <div class="lg:col-span-1 bg-white p-4 rounded shadow h-full">
    <h2 class="text-lg font-semibold mb-3">🚘 Biltyper</h2>
    <ul class="space-y-1 text-sm font-mono max-h-96 overflow-y-auto">
      <li v-for="bil in bilTyper" class="border-b pb-1">{{ bil.type }}</li>
    </ul>
  </div>
</template>

<script lang="js">
  import { defineComponent } from 'vue';
  import axios from 'axios';

  export default defineComponent({
    data() {
      return {
        loading: false,
        localApi: null,
        externApi: null,
        bilTyper: []
      };
    },
    async created() {
      // fetch the data when the view is created and the data is
      // already being observed
      await this.fetchData();
    },
    watch: {
      // call again the method if the route changes
      '$route': 'fetchData'
    },
    methods: {
      async fetchData() {
        //this.localApi = null;
        //this.externApi = null;
        //this.loading = true;
        //this.bilTyper = [];

        axios.get('/api/Biler')
        .then(response => {
          this.localApi = response.data;
          //console.log(response.data);
        },
          error => {
            console.log(error);
          }
        );

        console.log(this.localApi);
        //this.localApi.forEach(element =>
        //  //getSynsBilData(element['nummerplade'])
        //  console.log(element)
        //);
        //for (var k in this.localApi) {
        //  console.log(k);
        //}
        this.loading = false;
      }
    },
    computed: {
      getSynsBilData(element) {
        axios.get('https://api.synsbasen.dk/v1/vehicles/registration/' + element, {
          headers: {
            'Authorization': 'Bearer sb_sk_72ed12c2e4f9c8f4590032da932f2564',
            'Content-Type': 'application/json'
          }
        })
        .then(response => {
          this.bilTyper.push({ type: response.data['data']['brand_and_model'] })
          //response.data['data']['brand_and_model'].map(function (value, key) {
          //  this.bilTyper.push(value);
        });
      }
    },
  });
</script>

<style>
</style>
