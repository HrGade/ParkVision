<template>
  <body class="bg-gray-100 text-gray-800 font-sans">
    <div id="app" class="max-w-screen-xl mx-auto p-6">
      <h1 class="text-3xl font-bold mb-6 text-center">📈 Pi Kamera Dashboard</h1>

      <!-- Kontrolknapper -->
      <div class="flex flex-wrap justify-center gap-4 mb-8">
        <button @click="hentBilleder" class="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded">
          🔄 Opdater billeder
        </button> 
      </div>

      <div class="grid grid-cols-1 lg:grid-cols-4 gap-6">
        <!-- Seneste nummerplader (venstre kolonne) -->
        <div class="lg:col-span-1 bg-white p-4 rounded shadow h-full">
          <h2 class="text-lg font-semibold mb-3">🔍 Sidste 10 registreringer</h2>
          <ul class="space-y-1 text-sm font-mono max-h-96 overflow-y-auto">
            <li v-for="bil in post" :key="bil.Nummerplade" class="border-b pb-1">{{ bil.nummerplade }}</li>
          </ul>
        </div>

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
            <div class="bg-white p-6 rounded shadow">
              <h2 class="text-xl font-semibold mb-3 text-center">🚘 Biltyper</h2>
              <canvas id="chartTyper"></canvas>
            </div>

            <!-- Ledige pladser -->
            <div class="bg-white p-6 rounded shadow text-center">
              <h2 class="text-xl font-semibold mb-2">🅿️ Ledige pladser</h2>
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
  </body>
</template>

<script lang="js">
  import { defineComponent } from 'vue';

  const SYNBASE_API = 'https://api.synsbasen.dk/v1/vehicles';
  const API_KEY = 'sb_sk_72ed12c2e4f9c8f4590032da932f2564'; // husk at holde den hemmelig!

  export default defineComponent({
    data() {
      return {
        loading: false,
        post: [],
        billeder: [],
        maxPladser: 40
      };
    },

    async created() {
      await this.fetchData();
    },

    watch: {
      '$route': 'fetchData'
    },

    computed: {
      ledigePladser() {
        return this.maxPladser - (this.post ? this.post.length : 0);
      }
    },

    methods: {
      async fetchData() {
        this.loading = true;
        this.post = [];

        const response = await fetch('/api/Biler');
        if (!response.ok) {
          this.loading = false;
          return;
        }

        const biler = await response.json();

        const enriched = await Promise.all(
          biler.map(async bil => {
            const plade = bil.nummerplade?.toUpperCase();

            try {
              const res = await fetch(`${SYNBASE_API}?registration=${plade}`, {
                headers: {
                  'Authorization': `Bearer ${API_KEY}`,
                  'Content-Type': 'application/json'
                }
              });

              if (!res.ok) throw new Error('API fejl');

              const data = await res.json();

              return {
                ...bil,
                brand: data?.data?.brand,
                model: data?.data?.brand_and_model,
                fuel: data?.data?.fuel_type,
                category: data?.data?.category
              };
            } catch (err) {
              console.warn(`Fejl ved opslag af: ${plade}`, err);
              return bil; // fallback
            }
          })
        );

        this.post = enriched;
        this.loading = false;
      }
    }
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
