<template>
  <div id="app" class="max-w-screen-xl mx-auto p-6">
    <h1 class="text-3xl font-bold mb-6 text-center">📊 Pi Kamera Dashboard</h1>

    <!-- Kontrolknapper -->
    <div class="flex flex-wrap justify-center gap-4 mb-8">
      <button @click="hentBilleder" class="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded">
        🔄 Opdater billeder
      </button>
      <button @click="tagBillede" class="bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded">
        📸 Tag nyt billede
      </button>
    </div>

    <div class="grid grid-cols-1 lg:grid-cols-4 gap-6">
      <!-- Seneste nummerplader (venstre kolonne) -->
      <Registrations />

      <!-- Statistik og billeder (højre kolonne) -->
      <div class="lg:col-span-3 space-y-8">
        <!-- Statistiksektion -->
        <div class="grid grid-cols-1 md:grid-cols-3 gap-8">

          <!-- Kørselstype -->
          <div class="bg-white p-6 rounded shadow">
            <h2 class="text-xl font-semibold mb-3 text-center">🚗 Kørselstype</h2>
            <canvas id="chartKørsel"></canvas>
          </div>

          <!-- Biltyper -->
          <BilTyper />

          <!-- Ledige pladser -->
          <LedigePladser />

        </div>
      </div>

      <!-- Galleri -->
      <div>
        <h2 class="text-xl font-semibold mb-4">📸 Billedgalleri</h2>
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          <div v-for="billede in billeder" :key="billede" class="bg-white rounded shadow p-4">
            <img :src="`http://<pi_ip>:5000/images/${billede}`" :alt="billede" class="w-full h-48 object-cover rounded mb-2" />
            <p class="text-center text-sm font-mono">{{ billede }}</p>


          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
    import BilTyper from './BilTyper.vue';
    import Registrations from './Registrations.vue';
    import LedigePladser from './LedigePladser.vue';
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
