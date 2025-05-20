<script setup>
  import Dashboard from './components/Dashboard.vue'
</script>

<script lang="js">
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
  };
</script>

<template>
  <div class="wrapper">
    <Dashboard />
  </div>
</template>

<style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
