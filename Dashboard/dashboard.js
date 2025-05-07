const { createApp } = Vue;

createApp({
  data() {
    return {
      billeder: [],
      nummerplader: [],
      kørselStats: { erhverv: 0, privat: 0 },
      biltyper: { personbil: 0, suv: 0, varevogn: 0 },
      chartKørsel: null,
      chartTyper: null
    };
  },
  methods: {
    hentBilleder() {
      fetch("http://<pi_ip>:5000/images")
        .then(res => res.json())
        .then(data => {
          this.billeder = data;
        });
    },
    tagBillede() {
      fetch("http://<pi_ip>:5000/capture", { method: "POST" })
        .then(() => this.hentBilleder());
    },
    hentNummerplader() {
      fetch("http://<pi_ip>:5000/stats/last-plates")
        .then(res => res.json())
        .then(data => {
          this.nummerplader = data;
        });
    },
    hentKørselStats() {
      fetch("http://<pi_ip>:5000/stats/kørsel")
        .then(res => res.json())
        .then(data => {
          this.kørselStats = data;
          this.opdaterKørselChart();
        });
    },
    hentBilTyper() {
      fetch("http://<pi_ip>:5000/stats/typer")
        .then(res => res.json())
        .then(data => {
          this.biltyper = data;
          this.opdaterTyperChart();
        });
    },
    initKørselChart() {
      const ctx = document.getElementById('chartKørsel').getContext('2d');
      this.chartKørsel = new Chart(ctx, {
        type: 'doughnut',
        data: {
          labels: ['Erhverv', 'Privat'],
          datasets: [{
            data: [0, 0],
            backgroundColor: ['#10b981', '#3b82f6']
          }]
        },
        options: {
          plugins: {
            legend: { position: 'bottom' }
          }
        }
      });
    },
    initTyperChart() {
      const ctx = document.getElementById('chartTyper').getContext('2d');
      this.chartTyper = new Chart(ctx, {
        type: 'bar',
        data: {
          labels: ['Personbil', 'SUV', 'Varevogn'],
          datasets: [{
            label: 'Antal',
            data: [0, 0, 0],
            backgroundColor: ['#6366f1', '#f59e0b', '#ef4444']
          }]
        },
        options: {
          responsive: true,
          scales: {
            y: { beginAtZero: true }
          },
          plugins: {
            legend: { display: false }
          }
        }
      });
    },
    opdaterKørselChart() {
      this.chartKørsel.data.datasets[0].data = [
        this.kørselStats.erhverv,
        this.kørselStats.privat
      ];
      this.chartKørsel.update();
    },
    opdaterTyperChart() {
      this.chartTyper.data.datasets[0].data = [
        this.biltyper.personbil,
        this.biltyper.suv,
        this.biltyper.varevogn
      ];
      this.chartTyper.update();
    }
  },
  mounted() {
    this.initKørselChart();
    this.initTyperChart();
    this.hentBilleder();
    this.hentNummerplader();
    this.hentKørselStats();
    this.hentBilTyper();
  }
}).mount("#app");