<template>
  <div id="client_keywords_component" class="client_keywords_component">
    <div class="client_keywords_number">{{ keywords }}</div>
    <br />
    <div class="client_keywords">keywords</div>
  </div>
</template>

<script>
export default {
  name: "clientKeywordsComponent",
  data() {
    return {
      keywords: 268238,
    };
  },

  methods: {
    sortTable: function sortTable(col) {
      if (this.sortColumn === col) {
        this.ascending = !this.ascending;
      } else {
        this.ascending = true;
        this.sortColumn = col;
      }

      var ascending = this.ascending;

      this.rows.sort(function(a, b) {
        if (a[col] > b[col]) {
          return ascending ? 1 : -1;
        } else if (a[col] < b[col]) {
          return ascending ? -1 : 1;
        }
        return 0;
      });
    },
    get_rows: function get_rows() {
      var start = (this.currentPage - 1) * this.elementsPerPage;
      var end = start + this.elementsPerPage;
      return this.rows.slice(start, end);
    },
  },

  computed: {
    columns: function columns() {
      if (this.rows.length == 0) {
        return [];
      }
      return Object.keys(this.rows[0]);
    },
  },
};
</script>

<style lang="scss">
.client_keywords_component {
  background-color: $white;
  text-align: center;
}

.client_keywords_number {
  color: $green;
}

.client_keywords {
  color: $blue;
}
</style>
