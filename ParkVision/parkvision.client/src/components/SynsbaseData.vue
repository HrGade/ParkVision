<template>
  <div class="lg:col-span-1 bg-white p-4 rounded shadow h-full">
    <h2 class="text-lg font-semibold mb-3">🔍 Sidste 10 registreringer</h2>
    <ul class="space-y-1 text-sm font-mono max-h-96 overflow-y-auto">
      <li v-for="bil in post" :key="bil.Nummerplade" class="border-b pb-1">{{ bil.nummerplade }}</li>
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
        post: null
      };
    },
    async beforeCreate() {
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
        this.post = null;
        this.loading = true;

        axios.get('https://api.synsbasen.dk/v1/brands', {
          headers: {
            'Authorization': 'Bearer sb_sk_72ed12c2e4f9c8f4590032da932f2564',
            'Content-Type': 'application/json'
          }
        })
          .then(response => {
            this.post = response.data;
            this.$synsbasedata = response.data;
            this.loading = false;
            //console.log(response.data);
            //console.log(this.$synsbasedata)
          },
            error => {
              console.log(error);
            });
      }
    },
  });
</script>

<style>
</style>
