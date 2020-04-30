<template>
  <div id="client_intent_component" class="client_intent_component">
    <div class="chart">
      <svg viewbox="0 0 560 260">
        <defs>
          <filter id="dropshadow">
            <feGaussianBlur in="SourceAlpha" stdDeviation="3"></feGaussianBlur>
            <feOffset dx="0" dy="0" result="offsetblur"></feOffset>
            <feComponentTransfer>
              <feFuncA slope="0.2" type="linear"></feFuncA>
            </feComponentTransfer>
            <feMerge>
              <feMergeNode></feMergeNode>
              <feMergeNode in="SourceGraphic"></feMergeNode>
            </feMerge>
          </filter>
        </defs>
        <g class="datasets">
          <path
            class="dataset"
            d="M0,260 C0,260 4,252 7,252 C66,252 90,102 139,102 C188,102 205,135 252,135 C299,135 309,89 330,89 C350,89 366,122 404,122 C442,122 431,98 451,98 C470,98 499,213 560,260 L0,259 Z"
            id="dataset-3"
          ></path>
          <path
            class="dataset"
            d="M0,260 C35,254 63,124 88,124 C114,124 148,163 219,163 C290,163 315,100 359,100 C402,100 520,244 560,259 C560,259 0,259 0,260 Z"
            id="dataset-2"
          ></path>
          <path
            class="dataset"
            d="M0,260 C0,260 22,199 64,199 C105,199 112,144 154,144 C195,144 194,126 216,126 C237,126 263,184 314,184 C365,183 386,128 434,129 C483,130 511,240 560,260 L0,260 Z"
            id="dataset-1"
          ></path>
        </g>
      </svg>
    </div>
  </div>
</template>

<script>
export default {
  name: "clientIntentComponent",
  data() {
    return {};
  },
  methods: {},
};
</script>

<style lang="scss">
.client_intent_component {
  background-color: $white;
  text-align: center;
}

.chart::before,
.chart::after {
  display: inline-block;
  font-size: 0.875em;
}

.chart::before {
  content: "4k \a3k \a2k \a1k \a 0";
  white-space: pre;
  height: 100%;
  line-height: 3;
  text-align: right;
}

.chart::after {
  content: "07:00h 21:00h";
  width: 110%;
  margin-left: -5%;
  word-spacing: 36.5em;
  padding-top: 0.5em;
}

.dataset {
  fill-opacity: 0.8;
  -webkit-filter: url(#dropshadow);
  filter: url(#dropshadow);
}

#dataset-1 {
  fill: orange;
}

#dataset-2 {
  fill: red;
}

#dataset-3 {
  fill: green;
}

@-webkit-keyframes raise {
  0% {
    -webkit-transform: scaleY(0.01);
    transform: scaleY(0.01);
  }
  75% {
    -webkit-transform: scaleY(1.1);
    transform: scaleY(1.1);
  }
  100% {
    -webkit-transform: scaleY(1);
    transform: scaleY(1);
  }
}

@keyframes raise {
  0% {
    -webkit-transform: scaleY(0.01);
    transform: scaleY(0.01);
  }
  75% {
    -webkit-transform: scaleY(1.1);
    transform: scaleY(1.1);
  }
  100% {
    -webkit-transform: scaleY(1);
    transform: scaleY(1);
  }
}
.dataset {
  -webkit-transform-origin: bottom;
  transform-origin: bottom;
  -webkit-transform: scaleY(0.01);
  transform: scaleY(0.01);
  opacity: 0.5;
}

.dataset {
  opacity: 1;
  -webkit-animation: raise 0.5s ease 0.2s forwards;
  animation: raise 0.5s ease 0.2s forwards;
}

#dataset-1 {
  -webkit-animation-delay: 0.2s;
  animation-delay: 0.2s;
}

#dataset-2 {
  -webkit-animation-delay: 0.1s;
  animation-delay: 0.1s;
}

#dataset-3 {
  -webkit-animation-delay: 0s;
  animation-delay: 0s;
}

.client_keywords_number {
  color: $green;
}

.client_keywords {
  color: $blue;
}
</style>
