<template>
  <div class="bg-white p-6 rounded shadow text-center">
    <h2 class="text-xl font-semibold mb-2">🅿️ Ledige pladser på parkeringsplads 1</h2>
    <p class="text-4xl font-bold"
       :class="{
       'text-green-600' : ledigePladser>
      10,
      'text-yellow-500': ledigePladser <= 10 && ledigePladser > 3,
      'text-red-600': ledigePladser <= 3
      }"
      >
      {{ ledigePladser }} / {{ maxPladser }}
    </p>
  </div>
</template>

<script lang="js">
  import { defineComponent } from 'vue';
  import axios from 'axios';

  export default defineComponent({
    data() {
      return {
        loading: false,
        post: null,
        billeder: [],
        maxPladser: 0
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

        axios.get('/api/Parkeringspladser/1')
          .then(response => {
            this.maxPladser = response.data['ledigePladser'];
          },
            error => {
              console.log(error);
            });

        axios.get('/api/Biler')
          .then(response => {
            this.post = response.data;
            this.loading = false;
          },
            error => {
              console.log(error);
            });
      }
    },
    computed: {
      ledigePladser() {
        return this.maxPladser - (this.post ? this.post.length : 0);
      }
    }
  });
</script>

<style>
</style>
