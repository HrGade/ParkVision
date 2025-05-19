<template>
  <div class="lg:col-span-1 bg-white p-4 rounded shadow h-full">
    <h2 class="text-lg font-semibold mb-3">🔍 Sidste 10 registreringer</h2>
    <ul class="space-y-1 text-sm font-mono max-h-96 overflow-y-auto">
      <li v-for="bil in get" :key="bil.Nummerplade" class="border-b pb-1">{{ bil.nummerplade }}</li>
    </ul>
  </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    export default defineComponent({
        components: {
        },
        directives: {
        },
        filters: {
        },
        props: {
        },
        data() {
            return {
            }
        },
        computed: {
        },
        watch: {
        },
        beforeCreate() {
        },
        created() {
        },
        beforeMount() {
        },
        mounted() {
        },
        updated() {
        },
        activated() {
        },
        deactivated() {
        },
        beforeDestroy() {
        },
        destroyed() {
        },
        methods: {
        },
    });
</script>

<style>
</style>
