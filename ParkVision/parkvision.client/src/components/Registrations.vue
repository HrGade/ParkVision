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
        this.post = null;
        this.loading = true;

        axios.get('/api/Biler')
          .then(response => {
            this.post = response.data;
            this.loading = false;
            console.log(response.data);
          },
            error => {
              console.log(error);
            });
      }
    },

    //data() {
    //  return {
    //    loading: false, post: null, billeder: [], maxPladser: 40
    //  }
    //},
    //computed: {
    //  ledigePladser() {
    //    return this.maxPladser - (this.post ? this.post.length : 0);
    //  }

    //}
  });
</script>

<style scoped>
  th {
    font-weight: bold;
  }

  th, td {
    padding-left: .5rem;
    padding-right: .5rem;
  }

  .weather-component {
    text-align: center;
  }

  table {
    margin-left: auto;
    margin-right: auto;
  }
</style>
